import { MutationTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState } from '@/store/index.types';

// Keys
export enum HomeMutationTypes {
  UPDATE_HOME = 'UPDATE_HOME'
}
// Mutation Interface
export type HomeMutations<H = HomeState> = {
  [HomeMutationTypes.UPDATE_HOME](state: H, payload: Home): void;
};

// MutationType keys
export const mutations: MutationTree<HomeState> & HomeMutations = {
  [HomeMutationTypes.UPDATE_HOME](state, payload) {
    state.home = payload;
  }
};
