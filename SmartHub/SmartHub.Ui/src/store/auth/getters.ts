import { GetterTree } from 'vuex';
import { RootState, AuthState } from '@/store/index.types';

// Getter Types
//TODO move all auth stuff into one file?

// eslint-disable-next-line @typescript-eslint/ban-types
export type AuthGetters = {};

// Define Getters
export const getters: GetterTree<AuthState, RootState> & AuthGetters = {};
