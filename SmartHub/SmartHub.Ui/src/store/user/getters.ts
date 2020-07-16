import { GetterTree } from 'vuex';
import { AuthResponse, User } from '@/types/types';
import { RootState, UserState } from '@/store/index.types';

export const getters: GetterTree<UserState, RootState> = {
  getUser(state): User | undefined {
    return state.user;
  },
  getRole(state): string[] | undefined {
    return state.authResponse?.roles;
  },
  getAuthData(state): AuthResponse | undefined {
    return state.authResponse;
  },
  getAuthResponse(state): AuthResponse | undefined {
    return state.authResponse;
  }
};
