import { MutationTree } from 'vuex';
import { AppConfig } from '@/types/types';
import { AppState } from '@/store/index.types';

// Keys
export enum AppMutationTypes {
  // App
  UPDATE_APP = 'UPDATE_APP',
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_MOBILE_SIDEBAR = 'SET_MOBILE_SIDEBAR',
  SET_MINI_SIDEBAR = 'SET_MINI_SIDEBAR'
}
// Mutation Interface
export type AppMutations<A = AppState> = {
  // App
  [AppMutationTypes.UPDATE_APP](state: A, payload: AppConfig): void;
  // UI
  [AppMutationTypes.SET_USER_DROPDOWN](state: A, payload: boolean): void;
  [AppMutationTypes.SET_MOBILE_SIDEBAR](state: A, payload: boolean): void;
  [AppMutationTypes.SET_MINI_SIDEBAR](state: A, payload: boolean): void;
};

// MutationType keys
export const mutations: MutationTree<AppState> & AppMutations = {
  // App
  [AppMutationTypes.UPDATE_APP](state, payload) {
    state.appConfig = payload;
  },
  // UI
  [AppMutationTypes.SET_USER_DROPDOWN](state, payload) {
    state.userDropDownOpen = payload;
  },
  [AppMutationTypes.SET_MOBILE_SIDEBAR](state, payload) {
    state.mobileSidebarOpen = payload;
  },
  [AppMutationTypes.SET_MINI_SIDEBAR](state, payload) {
    state.miniSidebarOpen = payload;
  }
};
