import { MutationTree } from 'vuex';
import { AuthState } from '@/store/index.types';

// keys
export enum AuthMutationTypes {
  UPDATE_TOKEN = 'UPDATE_TOKEN'
}

// Mutations Interface
export type AuthMutations<A = AuthState> = {
  [AuthMutationTypes.UPDATE_TOKEN](state: A, payload?: string): void;
};

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [AuthMutationTypes.UPDATE_TOKEN](state, payload) {
    state.token = payload;
  }
};
