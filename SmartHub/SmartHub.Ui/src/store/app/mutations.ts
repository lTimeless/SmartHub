import { MutationTree } from 'vuex';
import { Group, Device, AppConfig } from '@/types/types';
import { AppState } from '@/store/index.types';

// Keys
export enum AppMutationTypes {
  UPDATE_APP = 'UPDATE_App',
  UPDATE_GROUPS = 'UPDATE_GROUPS',
  UPDATE_DEVICES = 'UPDATE_DEVICES',
  // Dropdowns
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_NOTIFICATION_DROPDOWN = 'SET_NOTIFICATION_DROPDOWN'
}
// Mutation Interface
export type AppMutations<A = AppState> = {
  [AppMutationTypes.UPDATE_APP](state: A, payload: AppConfig): void;
  [AppMutationTypes.UPDATE_GROUPS](state: A, payload: Group[]): void;
  [AppMutationTypes.UPDATE_DEVICES](state: A, payload: Device[]): void;
  // Dropdowns
  [AppMutationTypes.SET_USER_DROPDOWN](state: A, payload: boolean): void;
  [AppMutationTypes.SET_NOTIFICATION_DROPDOWN](state: A, payload: boolean): void;
};

// MutationType keys
export const mutations: MutationTree<AppState> & AppMutations = {
  [AppMutationTypes.UPDATE_APP](state, payload) {
    state.appConfig = payload;
  },
  [AppMutationTypes.UPDATE_GROUPS](state, payload) {
    state.groups = payload;
  },
  [AppMutationTypes.UPDATE_DEVICES](state, payload) {
    state.devices = payload;
  },
  [AppMutationTypes.SET_USER_DROPDOWN](state, payload) {
    state.userDropDownOpen = payload;
  },
  [AppMutationTypes.SET_NOTIFICATION_DROPDOWN](state, payload) {
    state.notificationDropdownOpen = payload;
  }
};
