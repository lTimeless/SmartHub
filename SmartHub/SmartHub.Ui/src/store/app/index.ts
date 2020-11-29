import { Module } from 'Vuex';
import { RootState, AppState } from '@/store/index.types';
import { getters } from '@/store/app/getters';
import { mutations } from '@/store/app/mutations';
import { actions } from '@/store/app/actions';

export const state: AppState = {
  notificationDropdownOpen: false,
  userDropDownOpen: false
};

export const appModule: Module<AppState, RootState> = {
  state,
  getters,
  actions,
  mutations
};
