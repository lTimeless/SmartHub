import { GetterTree } from 'vuex';
import { AppState, RootState } from '@/store/index.types';

// Getter Types
export type HomeGetters = {
  // getOnlyParentGroups(state: AppState): Group[] | undefined;
};

export const getters: GetterTree<AppState, RootState> = {};
