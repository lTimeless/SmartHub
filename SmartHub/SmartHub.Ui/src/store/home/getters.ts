import { GetterTree } from 'vuex';
import { Home } from '@/types/types';
import { HomeState, RootState } from '@/store/index.types';

export const getters: GetterTree<HomeState, RootState> = {
  getHome(state): Home | undefined {
    return state.home;
  }
};
