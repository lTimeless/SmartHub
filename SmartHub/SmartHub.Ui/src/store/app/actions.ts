import { ActionContext, ActionTree } from 'vuex';
import { RootState, AppState } from '@/store/index.types';
import { Device, Group } from '@/types/types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';
import { useQuery, useResult } from '@vue/apollo-composable';
import { GET_APP_CONFIG } from '@/graphql/queries';

// Keys
export enum AppActionTypes {
  // App
  GET_APP = 'GET_APP',
  // Group
  ADD_GROUP = 'ADD_GROUP',
  // Device
  ADD_DEVICE = 'ADD_DEVICE',
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_NOTIFICATION_DROPDOWN = 'SET_NOTIFICATION_DROPDOWN'
}

// actions context type
type ActionAugments = Omit<ActionContext<AppState, RootState>, 'commit'> & {
  commit<K extends keyof AppMutations>(
    key: K,
    payload: Parameters<AppMutations[K]>[1]
  ): ReturnType<AppMutations[K]>;
};

// Action Interface
export type HomeActions = {
  // App
  [AppActionTypes.GET_APP]({ commit }: ActionAugments): Promise<void>;
  // Group
  [AppActionTypes.ADD_GROUP]({ commit }: ActionAugments, payload: Group): Promise<void>;
  // Device
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
  // Group
  async [AppActionTypes.ADD_GROUP]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.ADD_GROUP, payload);
  },
  // Device
  async [AppActionTypes.ADD_DEVICE]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.ADD_DEVICE, payload);
  },
  // UI
  async [AppActionTypes.SET_NOTIFICATION_DROPDOWN]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_NOTIFICATION_DROPDOWN, payload);
  },
  async [AppActionTypes.SET_USER_DROPDOWN]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_USER_DROPDOWN, payload);
  }
};
