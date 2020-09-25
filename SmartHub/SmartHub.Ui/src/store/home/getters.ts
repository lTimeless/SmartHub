import { GetterTree } from 'vuex';
import { HomeState, RootState } from '@/store/index.types';
import { Home } from '@/types/types';

// Getter Types
export type HomeGetters = {
  getHome(state: HomeState): Home | null;
};

export const getters: GetterTree<HomeState, RootState> = {
  getHome: (state) => state.home
};
