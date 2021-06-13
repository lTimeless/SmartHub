import { ActionContext, ActionTree } from 'vuex';
import { RootState, AppState } from '@/store/index.types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';

// Keys
export enum AppActionTypes {
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN',
  SET_MOBILE_SIDEBAR = 'SET_MOBILE_SIDEBAR',
  SET_MINI_SIDEBAR = 'SET_MINI_SIDEBAR',
  RESET_SIDEBAR = 'RESET_SIDEBAR'
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
  // UI
  [AppActionTypes.SET_USER_DROPDOWN]({ commit }: ActionAugments, payload: boolean): Promise<void>;
  [AppActionTypes.SET_MOBILE_SIDEBAR]({ commit }: ActionAugments, payload: boolean): Promise<void>;
  [AppActionTypes.SET_MINI_SIDEBAR]({ commit }: ActionAugments, payload: boolean): Promise<void>;
  [AppActionTypes.RESET_SIDEBAR]({ commit }: ActionAugments): Promise<void>;
};

export const actions: ActionTree<AppState, RootState> = {
  // UI
  async [AppActionTypes.SET_USER_DROPDOWN]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_USER_DROPDOWN, payload);
  },
  async [AppActionTypes.SET_MOBILE_SIDEBAR]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_MOBILE_SIDEBAR, payload);
  },
  async [AppActionTypes.SET_MINI_SIDEBAR]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_MINI_SIDEBAR, payload);
  },
  // TODO do i need to reset it manually, because it is only set in appState ?
  async [AppActionTypes.RESET_SIDEBAR]({ commit }): Promise<void> {
    commit(AppMutationTypes.SET_MINI_SIDEBAR, true);
    commit(AppMutationTypes.SET_MOBILE_SIDEBAR, false);
  }
};
