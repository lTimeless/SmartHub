import { GetterTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';

export const getters: GetterTree<AuthState, RootState> = {
  getRole(state: AuthState): string[] | undefined {
    return state.authResponse?.roles;
  },
  getAuthResponse(state: AuthState): AuthResponse | null {
    return state.authResponse;
  }
};
