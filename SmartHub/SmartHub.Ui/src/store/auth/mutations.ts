import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { AuthState } from '@/store/index.types';

// MutationType keys
export const AUTH_USER = 'AUTH_USER';
export const UPDATE_REG_STEP = 'UPDATE_REG_STEP';

export const mutations: MutationTree<AuthState> = {
  [AUTH_USER](state, payload: AuthResponse) {
    state.authResponse = payload;
  },
  [UPDATE_REG_STEP](state, payload: number) {
    state.regStepIndex = payload;
  }
};
