import { Module } from 'vuex';
import { RootState, AppState } from '@/store/index.types';
import { getters } from '@/store/app/getters';
import { mutations } from '@/store/app/mutations';
import { actions } from '@/store/app/actions';

export const state: AppState = {};

export const appModule: Module<AppState, RootState> = {
  state,
  getters,
  actions,
  mutations
};
