import { MutationTree } from 'vuex';
import { AuthResponse, User } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export const M_AUTH = 'M_AUTH';
export const M_ME = 'M_AUTH_USER';

// Mutations Interface
export interface AuthMutations<A = AuthState> {
  [M_AUTH](state: A, payload: AuthResponse): void;
  [M_ME](state: A, payload: User): void;
}

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [M_AUTH](state: AuthState, payload: AuthResponse) {
    state.authResponse = payload;
  },
  [M_ME](state: AuthState, payload: User) {
    state.Me = payload;
  }
};
