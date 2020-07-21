import { GetterTree } from 'vuex';
import { User } from '@/types/types';
import { RootState, UserState } from '@/store/index.types';

export const getters: GetterTree<UserState, RootState> = {
  getUser(state: UserState): User | null {
    return state.user;
  }
};
