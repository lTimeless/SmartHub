import { MutationTree } from 'vuex';
import { Group, Device, AppConfig } from '@/types/types';
import { AppState } from '@/store/index.types';

// Keys
export enum AppMutationTypes {
  UPDATE_APP = 'UPDATE_App',
  UPDATE_GROUPS = 'UPDATE_GROUPS',
  UPDATE_Devices = 'UPDATE_Devices'
}
// Mutation Interface
export type AppMutations<A = AppState> = {
  [AppMutationTypes.UPDATE_APP](state: A, payload: AppConfig): void;
  [AppMutationTypes.UPDATE_GROUPS](state: A, payload: Group[]): void;
  [AppMutationTypes.UPDATE_Devices](state: A, payload: Device[]): void;
};

// MutationType keys
export const mutations: MutationTree<AppState> & AppMutations = {
  [AppMutationTypes.UPDATE_APP](state, payload) {
    state.appConfig = payload;
  },
  [AppMutationTypes.UPDATE_GROUPS](state, payload) {
    state.groups = payload;
  },
  [AppMutationTypes.UPDATE_Devices](state, payload) {
    state.devices = payload;
  }
};
