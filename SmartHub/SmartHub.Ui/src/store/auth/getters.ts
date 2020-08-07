import { GetterTree } from 'vuex';
import { RootState, AuthState } from '@/store/index.types';

// Getter Types
export type AuthGetters = {};

// Define Getters
export const getters: GetterTree<AuthState, RootState> & AuthGetters = {};
