import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export const M_AUTH_USER = 'M_AUTH_USER';

// Mutations Interface
export interface AuthMutations<A = AuthState> {
  [M_AUTH_USER](state: A, payload: AuthResponse): void;
}

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [M_AUTH_USER](state: AuthState, payload: AuthResponse) {
    state.authResponse = payload;
  }
};
