import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { user } from '@/store/user';
import { RootState } from '@/store/index.types';
import { home } from '@/store/home';

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
  modules: {
    user,
    home
  }
};

export default new Vuex.Store<RootState>(store);
