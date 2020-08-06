import { ActionContext, ActionTree } from 'vuex';
import axiosInstance from '@/router/axios/axios';
import { RootState, HomeState, AuthState } from '@/store/index.types';
import { Home, HomeCreateRequest, HomeUpdateRequest, ServerResponse } from '@/types/types';
import { getAuthentication } from '@/services/auth/authService';
import { HomeMutations } from '@/store/home/mutations';

// Keys
export const FETCH_HOME = 'FETCH_HOME';
export const CREATE_HOME = 'CREATE_HOME';
export const UPDATE_HOME = 'UPDATE_HOME';

// actions
type AugmentedActionContext = {
  commit<K extends keyof HomeMutations<AuthState>>(
    key: K,
    payload: Parameters<HomeMutations<AuthState>[K]>[1]
  ): ReturnType<HomeMutations<AuthState>[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

// Action Interface
export interface HomeActions {
  [FETCH_HOME]({ commit }: AugmentedActionContext): Promise<void>;
  [CREATE_HOME]({ commit }: AugmentedActionContext, payload: HomeCreateRequest): Promise<void>;
  [UPDATE_HOME]({ commit }: AugmentedActionContext, payload: HomeUpdateRequest): Promise<void>;
}

export const actions: ActionTree<HomeState, RootState> = {
  async [FETCH_HOME]({ commit }): Promise<void> {
    await axiosInstance
      .get<ServerResponse<Home>>('api/home')
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [CREATE_HOME]({ commit }, payload: HomeCreateRequest): Promise<void> {
    await axiosInstance
      .post<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [UPDATE_HOME]({ commit }, payload: HomeUpdateRequest): Promise<void> {
    console.log('axiosAction', axiosInstance.defaults);
    axiosInstance.defaults.headers = getAuthentication().headers;
    await axiosInstance
      .put<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
