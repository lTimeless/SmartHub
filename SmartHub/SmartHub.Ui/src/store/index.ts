import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { user } from '@/store/user';
import { RootState } from '@/store/index.types';

Vue.use(Vuex);

const store: StoreOptions<RootState> = { modules: { user } };

export default new Vuex.Store<RootState>(store);
