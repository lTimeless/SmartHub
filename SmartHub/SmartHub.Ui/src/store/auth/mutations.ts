import { MutationTree } from 'vuex';
import { User } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export enum AuthMutationTypes {
  TOKEN = 'TOKEN',
  User = 'User'
}

// Mutations Interface
export type AuthMutations<A = AuthState> = {
  [AuthMutationTypes.TOKEN](state: A, payload?: string): void;
  [AuthMutationTypes.User](state: A, payload?: User): void;
};

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [AuthMutationTypes.TOKEN](state, payload) {
    state.token = payload;
  },
  [AuthMutationTypes.User](state, payload) {
    state.user = payload;
  }
};
