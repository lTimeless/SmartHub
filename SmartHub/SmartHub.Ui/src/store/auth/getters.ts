import { GetterTree } from 'vuex';
import { AuthResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';

export const getters: GetterTree<AuthState, RootState> = {
  getRole(state): string[] | undefined {
    return state.authResponse?.roles;
  },
  getAuthResponse(state): AuthResponse | null {
    return state.authResponse;
  },
  getRegStepIndex(state): number {
    return state.regStepIndex;
  }
};
