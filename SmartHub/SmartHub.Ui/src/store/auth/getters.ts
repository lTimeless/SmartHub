import { GetterTree } from 'vuex';
import { RootState, AuthState } from '@/store/index.types';
import { User } from '@/types/types';

// Getter Types
export type AuthGetters = {
  getMe(state: AuthState): User | null | undefined;
};

// Define Getters
export const getters: GetterTree<AuthState, RootState> & AuthGetters = {
  getMe(state) {
    return state.user;
  }
};
