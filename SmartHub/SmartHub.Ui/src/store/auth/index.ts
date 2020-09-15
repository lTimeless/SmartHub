import { ModuleTree, Module } from 'vuex';
import { AuthState, RootState } from '@/store/index.types';
import { AuthGetters, getters } from '@/store/auth/getters';
import { actions, AuthActions } from '@/store/auth/actions';
import { AuthMutations, mutations } from '@/store/auth/mutations';

export const state: AuthState = {
  authResponse: null,
  whoAmI: null
};

export interface AuthModule<A = ModuleTree<AuthState>> {
  state: AuthState;
  getters: AuthGetters;
  actions: AuthActions;
  mutations: AuthMutations;
}

export const authModule: Module<AuthState, RootState> & AuthModule = {
  state,
  getters,
  actions,
  mutations
};
