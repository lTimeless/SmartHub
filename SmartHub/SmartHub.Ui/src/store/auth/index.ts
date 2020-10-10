import { Module } from 'vuex';
import { AuthState, RootState } from '@/store/index.types';
import { getters } from '@/store/auth/getters';
import { actions } from '@/store/auth/actions';
import { mutations } from '@/store/auth/mutations';
import { AuthResponse as Auth, Home, User } from '@/types/types';

export const state: AuthState = {};

export const authModule: Module<AuthState, RootState> = {
  state,
  getters,
  actions,
  mutations
};
