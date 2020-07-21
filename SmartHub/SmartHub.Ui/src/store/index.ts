import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { user } from '@/store/user';
import { RootState } from '@/store/index.types';
import { home } from '@/store/home';
import { auth } from '@/store/auth';

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
  modules: {
    user,
    home,
    auth
  }
};

export default new Vuex.Store<RootState>(store);
