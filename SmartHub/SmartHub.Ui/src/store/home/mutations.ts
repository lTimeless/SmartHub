import { MutationTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState } from '@/store/index.types';

// MutationType keys
export const UPDATE_HOME = 'UPDATE_HOME';

export const mutations: MutationTree<HomeState> = {
  [UPDATE_HOME](state: HomeState, payload: Home) {
    state.home = payload;
  }
};
