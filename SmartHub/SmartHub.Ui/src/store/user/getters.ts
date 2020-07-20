import { GetterTree } from 'vuex';
import { User } from '@/types/types';
import { RootState, UserState } from '@/store/index.types';

export const getters: GetterTree<UserState, RootState> = {
  getUser(state): User | null {
    return state.user;
  }
};
