import { GetterTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState, RootState } from '@/store/index.types';

export const getters: GetterTree<HomeState, RootState> = {
  getHome(state: HomeState): Home | undefined | null {
    return state.home;
  }
};
