import { MutationTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { UserState } from '@/store/index.types';

// MutationType keys
export const AUTH_USER = 'AUTH_USER';

export const mutations: MutationTree<UserState> = {
  [AUTH_USER](state, payload: AuthResponse) {
    state.authResponse = payload;
  }
};
