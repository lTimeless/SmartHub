import { ActionContext, ActionTree } from 'vuex';
import { RootState, AppState } from '@/store/index.types';
import { AppMutations, AppMutationTypes } from '@/store/app/mutations';

// Keys
export enum AppActionTypes {
  // UI
  SET_USER_DROPDOWN = 'SET_USER_DROPDOWN'
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
};

export const actions: ActionTree<AppState, RootState> = {
  // UI
  async [AppActionTypes.SET_USER_DROPDOWN]({ commit }, payload): Promise<void> {
    commit(AppMutationTypes.SET_USER_DROPDOWN, payload);
  }
};
