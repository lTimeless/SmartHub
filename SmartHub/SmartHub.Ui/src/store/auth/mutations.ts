import { MutationTree } from 'vuex';
import { AuthResponse, User } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export enum AuthMutationTypes {
  AUTH = 'AUTH',
  ME = 'ME'
}

// Mutations Interface
export type AuthMutations<A = AuthState> = {
  [AuthMutationTypes.AUTH](state: A, payload: AuthResponse): void;
  [AuthMutationTypes.ME](state: A, payload: User): void;
};

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [AuthMutationTypes.AUTH](state, payload) {
    state.authResponse = payload;
  },
  [AuthMutationTypes.ME](state, payload) {
    state.Me = payload;
  }
};
