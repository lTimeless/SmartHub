import { MutationTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState } from '@/store/index.types';

// Keys
export const M_UPDATE_HOME = 'M_UPDATE_HOME';

// Mutation Interface
export interface HomeMutations<H = HomeState> {
  [M_UPDATE_HOME](state: H, payload: Home): void;
}

// MutationType keys
export const mutations: MutationTree<HomeState> & HomeMutations = {
  [M_UPDATE_HOME](state: HomeState, payload: Home) {
    state.home = payload;
  }
};
