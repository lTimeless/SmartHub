import { ActionContext, ActionTree } from 'vuex';
import { RootState, AppState, AuthState } from '@/store/index.types';
import { AppConfig, AppConfigInitInput, Device, Group } from '@/types/types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';
// import { postInit } from '@/services/apis/init';
import { useQuery, useResult } from '@vue/apollo-composable';
import { getDevices, getGroups, GET_APP_CONFIG } from '@/graphql/queries';

// Keys
export enum AppActionTypes {
  // App
  GET_APP = 'GET_APP',
  InitIALIZE_APP = 'CREATE_App',
  UPDATE_APP = 'UPDATE_App',
  // Group
  GET_GROUPS = 'GET_GROUPS',
  ADD_GROUP = 'ADD_GROUP',
  // Device
  GET_DEVICES = 'GET_DEVICES',
  ADD_DEVICE = 'ADD_DEVICE',
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
  [AppActionTypes.InitIALIZE_APP]({ commit }: ActionAugments, payload: AppConfigInitInput): Promise<void>;
  [AppActionTypes.UPDATE_APP]({ commit }: ActionAugments, payload: AppConfig): Promise<void>;
  // Group
  [AppActionTypes.GET_GROUPS]({ commit }: ActionAugments): Promise<void>;
  [AppActionTypes.ADD_GROUP]({ commit }: ActionAugments, payload: Group): Promise<void>;
  // Device
  [AppActionTypes.GET_DEVICES]({ commit }: ActionAugments): Promise<void>;
  [AppActionTypes.ADD_DEVICE]({ commit }: ActionAugments, payload: Device): Promise<void>;
  // UI
  [AppActionTypes.SET_NOTIFICATION_DROPDOWN]({ commit }: ActionAugments, payload: boolean): Promise<void>;
  [AppActionTypes.SET_USER_DROPDOWN]({ commit }: ActionAugments, payload: boolean): Promise<void>;
};

export const actions: ActionTree<AppState, RootState> = {
  // App
  async [AppActionTypes.GET_APP]({ commit }): Promise<void> {
    const { result } = useQuery(GET_APP_CONFIG);
    const appConfig = useResult(result, null, (data) => data.appConfig);
    commit(AppMutationTypes.UPDATE_APP, appConfig);
  },
  async [AppActionTypes.InitIALIZE_APP]({ commit }, payload): Promise<void> {
    // await postInit(payload)
    //   .then((response) => {
    //     if (!response.success) {
    //       return Promise.reject(response.message);
    //     }
    //     commit(AppMutationTypes.UPDATE_APP, response.data);
    //     return Promise.resolve(response.data);
    //   })
    //   .catch((error) => Promise.reject(error));
  },
  async [AppActionTypes.UPDATE_APP]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.UPDATE_APP, payload);
  },
  // Group
  async [AppActionTypes.GET_GROUPS]({ commit }): Promise<void> {
    const { result } = useQuery(getGroups);
    const groups = useResult(result, null, (data) => data.groups);
    commit(AppMutationTypes.SET_GROUPS, groups);
  },
  async [AppActionTypes.ADD_GROUP]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.SET_GROUPS, payload);
  },
  // Device
  async [AppActionTypes.GET_DEVICES]({ commit }): Promise<void> {
    const { result } = useQuery(getDevices);
    const devices = useResult(result, null, (data) => data.devices);
    commit(AppMutationTypes.SET_DEVICES, devices);
  },
  async [AppActionTypes.ADD_DEVICE]({ commit }, payload): Promise<void> {
    // await commit(AppMutationTypes.UPDATE_GROUPS, payload);
  },
  // UI
  async [AppActionTypes.SET_NOTIFICATION_DROPDOWN]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.SET_NOTIFICATION_DROPDOWN, payload);
  },
  async [AppActionTypes.SET_USER_DROPDOWN]({ commit }, payload): Promise<void> {
    await commit(AppMutationTypes.SET_USER_DROPDOWN, payload);
  }
};
