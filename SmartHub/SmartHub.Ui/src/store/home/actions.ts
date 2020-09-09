import { ActionContext, ActionTree } from 'vuex';
import { RootState, HomeState, AuthState } from '@/store/index.types';
import { HomeCreateRequest, HomeUpdateRequest } from '@/types/types';
import { HomeMutations, M_UPDATE_HOME } from '@/store/home/mutations';
import { getHome, postHome, putHome } from '@/services/apis/home.service';

// Keys
export const A_FETCH_HOME = 'A_FETCH_HOME';
export const A_CREATE_HOME = 'A_CREATE_HOME';
export const A_UPDATE_HOME = 'A_UPDATE_HOME';

// actions context type
type AugmentedActionContext = {
  commit<K extends keyof HomeMutations<AuthState>>(key: K, payload: Parameters<HomeMutations<AuthState>[K]>[1]): ReturnType<HomeMutations<AuthState>[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

// Action Interface
export interface HomeActions {
  [A_FETCH_HOME]({ commit }: AugmentedActionContext): Promise<void>;
  [A_CREATE_HOME]({ commit }: AugmentedActionContext, payload: HomeCreateRequest): Promise<void>;
  [A_UPDATE_HOME]({ commit }: AugmentedActionContext, payload: HomeUpdateRequest): Promise<void>;
}

export const actions: ActionTree<HomeState, RootState> = {
  async [A_FETCH_HOME]({ commit }): Promise<void> {
    await getHome()
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(M_UPDATE_HOME, response.data);
        return Promise.resolve();
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [A_CREATE_HOME]({ commit }, payload: HomeCreateRequest): Promise<void> {
    await postHome(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(M_UPDATE_HOME, response.data);
        return Promise.resolve(response.data);
      })
      .catch((error) => Promise.reject(error));
  },
  async [A_UPDATE_HOME]({ commit }, payload: HomeUpdateRequest): Promise<void> {
    await putHome(payload)
      .then((res) => {
        commit(M_UPDATE_HOME, res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
