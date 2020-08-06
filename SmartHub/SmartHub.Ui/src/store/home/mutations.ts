import { MutationTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState } from '@/store/index.types';

// Keys
export const UPDATE_HOME = 'UPDATE_HOME';

// Mutation Interface
export interface HomeMutations<H = HomeState> {
  [UPDATE_HOME](state: H, payload: Home): void;
}

// MutationType keys
export const mutations: MutationTree<HomeState> & HomeMutations = {
  [UPDATE_HOME](state: HomeState, payload: Home) {
    state.home = payload;
  }
};
