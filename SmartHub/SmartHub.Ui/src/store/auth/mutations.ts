import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { AuthState } from '@/store/index.types';

// Keys
export const AUTH_USER = 'AUTH_USER';
export const UPDATE_SIGNIN_BTN = 'UPDATE_SIGNIN_BTN';

// Mutations Interface
export interface AuthMutations<A = AuthState> {
  [AUTH_USER](state: A, payload: AuthResponse): void;
  [UPDATE_SIGNIN_BTN](state: A): void;
}

// Define Mutations
export const mutations: MutationTree<AuthState> & AuthMutations = {
  [AUTH_USER](state: AuthState, payload: AuthResponse) {
    state.authResponse = payload;
  },
  [UPDATE_SIGNIN_BTN](state: AuthState) {
    state.isSignInBtnClicked = !state.isSignInBtnClicked;
  }
};
