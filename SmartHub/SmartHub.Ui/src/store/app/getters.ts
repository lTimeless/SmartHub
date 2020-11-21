import { GetterTree } from 'vuex';
import { AppState, RootState } from '@/store/index.types';

// Getter Types
export type HomeGetters = {
  // getHome(state: HomeState): Home | null | undefined;
};

export const getters: GetterTree<AppState, RootState> = {
  // getHome: (state) => state.home
};
