import { ActionTree } from 'vuex';
import axiosInstance from '@/router/axios/axios';
import { RootState, HomeState } from '@/store/index.types';
import { Home, HomeCreateRequest, HomeUpdateRequest, ServerResponse } from '@/types/types';
import { getAuthentication } from '@/services/auth/authService';

// ActionType keys
export const FETCH_HOME = 'GET_HOME';
export const CREATE_HOME = 'CREATE_HOME';
export const UPDATE_HOME = 'UPDATE_HOME';

export const actions: ActionTree<HomeState, RootState> = {
  async [FETCH_HOME]({ commit }): Promise<void> {
    await axiosInstance
      .get<ServerResponse<Home>>('api/home')
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [CREATE_HOME]({ commit }, payload: HomeCreateRequest): Promise<void> {
    await axiosInstance
      .post<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [UPDATE_HOME]({ commit }, payload: HomeUpdateRequest): Promise<void> {
    console.log('axiosAction', axiosInstance.defaults);
    axiosInstance.defaults.headers = getAuthentication().headers;
    await axiosInstance
      .put<ServerResponse<Home>>('api/home', payload)
      .then((response) => {
        commit(UPDATE_HOME, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
