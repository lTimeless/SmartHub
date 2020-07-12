import { MutationTree } from 'vuex';
import { LoginResponse, ServiceResponse, User } from '@/types/types';
import { UserState } from '@/store/index.types';

// MutationType keys
export const UPDATE_USER = 'UPDATE_USER';
export const LOGIN_USER = 'LOGIN_USER';

export const mutations: MutationTree<UserState> = {
  [UPDATE_USER](state, payload: User) {
    state.user = payload;
  },
  [LOGIN_USER](state, payload: ServiceResponse<LoginResponse>) {
    state.loginResponse = payload;
  }
};
