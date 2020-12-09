import { MutationTree } from 'vuex';
import { User } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export enum AuthMutationTypes {
  UPDATE_TOKEN = 'UPDATE_TOKEN',
  UPDATE_ME = 'UPDATE_ME'
}

// Mutations Interface
export type AuthMutations<A = AuthState> = {
  [AuthMutationTypes.UPDATE_TOKEN](state: A, payload?: string): void;
  [AuthMutationTypes.UPDATE_ME](state: A, payload?: User): void;
};

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [AuthMutationTypes.UPDATE_TOKEN](state, payload) {
    state.token = payload;
  },
  [AuthMutationTypes.UPDATE_ME](state, payload) {
    state.user = payload;
  }
};
