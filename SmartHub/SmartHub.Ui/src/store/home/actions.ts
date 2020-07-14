import { ActionTree } from 'vuex';
import Vue from 'vue';
import { RootState, HomeState } from '@/store/index.types';
import { Home, ServiceResponse } from '@/types/types';
import { UPDATE_HOME } from '@/store/home/mutations';

// ActionType keys
export const GET_HOME = 'GET_HOME';

export const actions: ActionTree<HomeState, RootState> = {
  async [GET_HOME]({ commit }) {
    await Vue.axios
      .get<ServiceResponse<Home>>('api/home')
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
