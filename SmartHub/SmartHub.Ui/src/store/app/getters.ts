import { GetterTree } from 'Vuex';
import { AppState, RootState } from '@/store/index.types';

// Getter Types
export type HomeGetters = {
  getGroupsAmount(state: AppState): number;
  getDevicesAmount(state: AppState): number;
};

export const getters: GetterTree<AppState, RootState> = {
  getGroupsAmount: (state) => (state.groups === undefined ? 0 : state.groups.length),
  getDevicesAmount: (state) => (state.devices === undefined ? 0 : state.devices.length)
};
