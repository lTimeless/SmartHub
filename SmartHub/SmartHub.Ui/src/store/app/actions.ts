import { ActionContext, ActionTree } from 'Vuex';
import { RootState, AppState, AuthState } from '@/store/index.types';
import { AppConfigInitRequest } from '@/types/types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';
import { postInit } from '@/services/apis/init';
import { getAppConfig } from '@/services/apis/appConfig';

// Keys
export enum AppActionTypes {
  GET_APP = 'GET_APP',
  CREATE_APP = 'CREATE_App'
}

// actions context type
type ActionAugments = Omit<ActionContext<AuthState, RootState>, 'commit'> & {
  commit<K extends keyof AppMutations>(
    key: K,
    payload: Parameters<AppMutations[K]>[1]
  ): ReturnType<AppMutations[K]>;
};

// Action Interface
export type HomeActions = {
  [AppActionTypes.GET_APP]({ commit }: ActionAugments): Promise<void>;
  [AppActionTypes.CREATE_APP]({ commit }: ActionAugments, payload: AppConfigInitRequest): Promise<void>;
};

export const actions: ActionTree<AppState, RootState> = {
  async [AppActionTypes.GET_APP]({ commit }): Promise<void> {
    await getAppConfig()
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(AppMutationTypes.UPDATE_APP, response.data);
        return Promise.resolve(response.data);
      })
      .catch((error) => Promise.reject(error));
  },
  async [AppActionTypes.CREATE_APP]({ commit }, payload): Promise<void> {
    await postInit(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(AppMutationTypes.UPDATE_APP, response.data);
        return Promise.resolve(response.data);
      })
      .catch((error) => Promise.reject(error));
  }
};
