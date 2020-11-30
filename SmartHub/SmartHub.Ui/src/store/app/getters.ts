import { GetterTree } from 'Vuex';
import { AppState, RootState } from '@/store/index.types';
import { Group } from '@/types/types';

// Getter Types
export type HomeGetters = {
  getOnlyParentGroups(state: AppState): Group[] | undefined;
  getGroupsAmount(state: AppState): number;
  getDevicesAmount(state: AppState): number;
};

export const getters: GetterTree<AppState, RootState> = {
  getOnlyParentGroups: (state) => state.groups?.filter((x: Group) => !x.isSubGroup),
  getGroupsAmount: (state) => (state.groups === undefined ? 0 : state.groups.length),
  getDevicesAmount: (state) => (state.devices === undefined ? 0 : state.devices.length)
};
