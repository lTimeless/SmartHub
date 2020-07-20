import { ActionTree } from 'vuex';
import Vue from 'vue';
import { RootState, HomeState } from '@/store/index.types';
import { Home, HomeCreateRequest, HomeUpdateRequest, Response } from '@/types/types';

// ActionType keys
export const FETCH_HOME = 'GET_HOME';
export const CREATE_HOME = 'CREATE_HOME';
export const UPDATE_HOME = 'UPDATE_HOME';

export const actions: ActionTree<HomeState, RootState> = {
  async [FETCH_HOME]({ commit }): Promise<void> {
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
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [UPDATE_HOME]({ commit }, payload: HomeUpdateRequest): Promise<void> {
    await Vue.axios
      .put<Response<Home>>('api/home', payload)
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
