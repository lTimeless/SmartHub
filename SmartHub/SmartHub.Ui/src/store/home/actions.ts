import { ActionContext, ActionTree } from 'vuex';
import axiosInstance from '@/router/axios/axios';
import { RootState, HomeState, AuthState } from '@/store/index.types';
import { Home, HomeCreateRequest, HomeUpdateRequest, ServerResponse } from '@/types/types';
import { getAuthentication } from '@/services/auth/authService';
import { HomeMutations, M_UPDATE_HOME } from '@/store/home/mutations';

// Keys
export const A_FETCH_HOME = 'A_FETCH_HOME';
export const A_CREATE_HOME = 'A_CREATE_HOME';
export const A_UPDATE_HOME = 'A_UPDATE_HOME';

// actions context type
type AugmentedActionContext = {
  commit<K extends keyof HomeMutations<AuthState>>(
    key: K,
    payload: Parameters<HomeMutations<AuthState>[K]>[1]
  ): ReturnType<HomeMutations<AuthState>[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

// Action Interface
export interface HomeActions {
  [A_FETCH_HOME]({ commit }: AugmentedActionContext): Promise<void>;
  [A_CREATE_HOME]({ commit }: AugmentedActionContext, payload: HomeCreateRequest): Promise<void>;
  [A_UPDATE_HOME]({ commit }: AugmentedActionContext, payload: HomeUpdateRequest): Promise<void>;
}

export const actions: ActionTree<HomeState, RootState> = {
  async [A_FETCH_HOME]({ commit }): Promise<void> {
    await axiosInstance
      .get<ServerResponse<Home>>('api/home')
      .then((response) => {
        commit(M_UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [A_CREATE_HOME]({ commit }, payload: HomeCreateRequest): Promise<void> {
    await axiosInstance
      .post<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(M_UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [A_UPDATE_HOME]({ commit }, payload: HomeUpdateRequest): Promise<void> {
    axiosInstance.defaults.headers = getAuthentication().headers;
    await axiosInstance
      .put<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(M_UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
