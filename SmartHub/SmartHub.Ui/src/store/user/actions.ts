import { ActionTree } from 'vuex';
import { LoginRequest, LoginResponse, ServiceResponse } from '@/types/types';
import Vue from 'vue';
import { LOGIN_USER } from '@/store/user/mutations';
import { RootState, UserState } from '@/store/index.types';

// ActionType keys
export const LOGIN = 'LOGIN';

export const actions: ActionTree<UserState, RootState> = {
  async [LOGIN]({ commit }, payload: LoginRequest) {
    await Vue.axios
      .post<ServiceResponse<LoginResponse>>('api/Identity/login', payload)
      .then((response) => {
        localStorage.setItem('loginResponse', JSON.stringify(response.data.data));
        commit(LOGIN_USER, response.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
