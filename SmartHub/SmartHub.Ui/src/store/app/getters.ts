import { GetterTree } from 'vuex';
import { AppState, RootState } from '@/store/index.types';
import { Group } from '@/types/types';

// Getter Types
export type HomeGetters = {
  getOnlyParentGroups(state: AppState): Group[] | undefined;
  getParentGroupsAmount(state: AppState): number;
  getSubGroupsAmount(state: AppState): number;
  getDevicesAmount(state: AppState): number;
};

export const getters: GetterTree<AppState, RootState> = {
  getOnlyParentGroups: (state) => state.groups?.filter((x: Group) => !x.isSubGroup),
  getParentGroupsAmount: (state) =>
    typeof state.groups === 'undefined' ? 0 : state.groups?.filter((x) => !x.isSubGroup).length,
  getSubGroupsAmount: (state) =>
    typeof state.groups === 'undefined' ? 0 : state.groups?.filter((x) => x.isSubGroup).length,
  getDevicesAmount: (state) => (typeof state.devices === 'undefined' ? 0 : state.devices?.length)
};
