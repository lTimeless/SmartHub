import { ActionContext, ActionTree } from 'vuex';
import { RootState, AppState, AuthState } from '@/store/index.types';
import { AppConfig, AppConfigInitRequest, Device, Group } from '@/types/types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';
import { postInit } from '@/services/apis/init';
import { getAppConfig } from '@/services/apis/appConfig';

// Keys
export enum AppActionTypes {
  // App
  GET_APP = 'GET_APP',
  CREATE_APP = 'CREATE_App',
  UPDATE_APP = 'UPDATE_App',
  // Group
  UPDATE_GROUPS = 'UPDATE_GROUPS',
  // Device
  UPDATE_Devices = 'UPDATE_Devices',
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_NOTIFICATION_DROPDOWN = 'SET_NOTIFICATION_DROPDOWN'
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
  // App
  [AppActionTypes.GET_APP]({ commit }: ActionAugments): Promise<void>;
  [AppActionTypes.CREATE_APP]({ commit }: ActionAugments, payload: AppConfigInitRequest): Promise<void>;
  [AppActionTypes.UPDATE_APP]({ commit }: ActionAugments, payload: AppConfig): Promise<void>;
  // Group
  [AppActionTypes.UPDATE_GROUPS]({ commit }: ActionAugments, payload: Group[]): Promise<void>;
  // Device
  [AppActionTypes.UPDATE_Devices]({ commit }: ActionAugments, payload: Device[]): Promise<void>;
  // UI
  [AppActionTypes.SET_NOTIFICATION_DROPDOWN]({ commit }: ActionAugments, payload: boolean): Promise<void>;
  [AppActionTypes.SET_USER_DROPDOWN]({ commit }: ActionAugments, payload: boolean): Promise<void>;
};

export const actions: ActionTree<AppState, RootState> = {
  // App
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
  },
  async [AppActionTypes.UPDATE_APP]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.UPDATE_APP, payload);
  },
  // Group
  async [AppActionTypes.UPDATE_GROUPS]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.UPDATE_GROUPS, payload);
  },
  // Device
  async [AppActionTypes.UPDATE_Devices]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.UPDATE_Devices, payload);
  },
  // UI
  async [AppActionTypes.SET_NOTIFICATION_DROPDOWN]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.SET_NOTIFICATION_DROPDOWN, payload);
  },
  async [AppActionTypes.SET_USER_DROPDOWN]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.SET_USER_DROPDOWN, payload);
  }
};
