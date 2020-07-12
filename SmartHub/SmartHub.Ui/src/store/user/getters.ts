import { GetterTree } from 'vuex';
import { LoginResponse, ServiceResponse, User } from '@/types/types';
import { RootState, UserState } from '@/store/index.types';

export const getters: GetterTree<UserState, RootState> = {
  getUser(state): User | undefined {
    return state.user;
  },
  getRole(state): string[] | undefined {
    return state.loginResponse?.data.roles;
  },
  getLogin(state): LoginResponse | undefined {
    return state.loginResponse?.data;
  },
  getLoginResponse(state): ServiceResponse<LoginResponse> | undefined {
    return state.loginResponse;
  }
};
