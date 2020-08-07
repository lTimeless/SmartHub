import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { AuthState } from '@/store/index.types';

// keys
export const M_AUTH_USER = 'M_AUTH_USER';
export const M_UPDATE_LOGIN_BTN = 'M_UPDATE_LOGIN_BTN';

// Mutations Interface
export interface AuthMutations<A = AuthState> {
  [M_AUTH_USER](state: A, payload: AuthResponse): void;
  [M_UPDATE_LOGIN_BTN](state: A): void;
}

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [M_AUTH_USER](state: AuthState, payload: AuthResponse) {
    state.authResponse = payload;
  },
  [M_UPDATE_LOGIN_BTN](state: AuthState) {
    state.isSignInBtnClicked = !state.isSignInBtnClicked;
  }
};
