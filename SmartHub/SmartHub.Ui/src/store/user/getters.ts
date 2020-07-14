import { GetterTree } from 'vuex';
import { AuthResponse, ServiceResponse, User } from '@/types/types';
import { RootState, UserState } from '@/store/index.types';

export const getters: GetterTree<UserState, RootState> = {
  getUser(state): User | undefined {
    return state.user;
  },
  getRole(state): string[] | undefined {
    return state.authResponse?.data.roles;
  },
  getAuthData(state): AuthResponse | undefined {
    return state.authResponse?.data;
  },
  getAuthResponse(state): ServiceResponse<AuthResponse> | undefined {
    return state.authResponse;
  }
};
