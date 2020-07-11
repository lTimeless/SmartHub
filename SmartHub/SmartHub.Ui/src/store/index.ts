import Vue from "vue";
import Vuex, {StoreOptions} from "vuex";
import { user } from "@/store/user";

Vue.use(Vuex);

export interface RootState {
}

const store: StoreOptions<RootState> = {
  modules: {
    user,
  },
};

export default new Vuex.Store<RootState>(store);
