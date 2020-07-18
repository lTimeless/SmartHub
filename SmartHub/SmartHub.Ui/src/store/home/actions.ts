import { ActionTree } from 'vuex';
import Vue from 'vue';
import { RootState, HomeState } from '@/store/index.types';
import { Home, HomeCreateRequest, Response } from '@/types/types';
import { UPDATE_HOME } from '@/store/home/mutations';

// ActionType keys
export const GET_HOME = 'GET_HOME';
export const CREATE_HOME = 'CREATE_HOME';

export const actions: ActionTree<HomeState, RootState> = {
  async [GET_HOME]({ commit }): Promise<void> {
    await Vue.axios
      .get<Response<Home>>('api/home')
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [CREATE_HOME]({ commit }, payload: HomeCreateRequest): Promise<void> {
    await Vue.axios
      .post<Response<Home>>('api/home', payload)
      .then((response) => {
        console.log(response);
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
