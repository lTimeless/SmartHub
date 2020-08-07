import { GetterTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState, RootState } from '@/store/index.types';

// Getter Types
export type HomeGetters = {
  getHome(state: HomeState): Home | null;
};

export const getters: GetterTree<HomeState, RootState> = {
  getHome(state: HomeState): Home | null {
    return state.home;
  }
};
