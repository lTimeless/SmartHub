import { MutationTree } from 'vuex';
import { Group, Device, AppConfig, User } from '@/types/types';
import { AppState } from '@/store/index.types';

// Keys
export enum AppMutationTypes {
  // App
  UPDATE_APP = 'UPDATE_APP',
  // Group
  SET_GROUPS = 'SET_GROUPS',
  ADD_GROUP = 'ADD_GROUP',
  // Device
  SET_DEVICES = 'SET_DEVICES',
  ADD_DEVICE = 'ADD_DEVICE',
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_NOTIFICATION_DROPDOWN = 'SET_NOTIFICATION_DROPDOWN'
}
// Mutation Interface
export type AppMutations<A = AppState> = {
  // UI
  [AppMutationTypes.UPDATE_APP](state: A, payload: AppConfig): void;
  // Group
  [AppMutationTypes.SET_GROUPS](state: A, payload: Group[]): void;
  [AppMutationTypes.ADD_GROUP](state: A, payload: Group): void;
  // Device
  [AppMutationTypes.SET_DEVICES](state: A, payload: Device[]): void;
  [AppMutationTypes.ADD_DEVICE](state: A, payload: Device): void;
  // UI
  [AppMutationTypes.SET_USER_DROPDOWN](state: A, payload: boolean): void;
  [AppMutationTypes.SET_NOTIFICATION_DROPDOWN](state: A, payload: boolean): void;
};

// MutationType keys
export const mutations: MutationTree<AppState> & AppMutations = {
  // App
  [AppMutationTypes.UPDATE_APP](state, payload) {
    state.appConfig = payload;
  },
  // Group
  [AppMutationTypes.SET_GROUPS](state, payload) {
    state.groups = payload;
  },
  [AppMutationTypes.ADD_GROUP](state, payload) {
    state.groups?.push(payload);
  },
  // Device
  [AppMutationTypes.SET_DEVICES](state, payload) {
    state.devices = payload;
  },
  [AppMutationTypes.ADD_DEVICE](state, payload) {
    state.devices?.push(payload);
  },
  // UI
  [AppMutationTypes.SET_USER_DROPDOWN](state, payload) {
    state.userDropDownOpen = payload;
  },
  [AppMutationTypes.SET_NOTIFICATION_DROPDOWN](state, payload) {
    state.notificationDropdownOpen = payload;
  }
};
