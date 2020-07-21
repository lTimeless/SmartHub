import { Module } from 'vuex';
import { mutations } from '@/store/user/mutations';
import { getters } from '@/store/user/getters';
import { actions } from '@/store/user/actions';
import { RootState, UserState } from '@/store/index.types';

export const state: UserState = {
  user: null
};

export const user: Module<UserState, RootState> = {
  state,
  getters,
  actions,
  mutations
};
