import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { AuthState } from '@/store/index.types';

// MutationType keys
export const AUTH_USER = 'AUTH_USER';
export const UPDATE_SIGNIN_BTN = 'UPDATE_SIGNIN_BTN';

export const mutations: MutationTree<AuthState> = {
  [AUTH_USER](state: AuthState, payload: AuthResponse) {
    state.authResponse = payload;
  },
  [UPDATE_SIGNIN_BTN](state: AuthState) {
    state.isSignInBtnClicked = !state.isSignInBtnClicked;
  }
};