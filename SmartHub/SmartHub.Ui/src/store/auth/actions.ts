import { ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, Response } from '@/types/types';
import Vue from 'vue';

import { RootState, AuthState } from '@/store/index.types';
import { AUTH_USER } from '@/store/auth/mutations';

// ActionType keys
export const LOGIN = 'LOGIN';
export const REGISTRATION = 'REGISTRATION';

export const actions: ActionTree<AuthState, RootState> = {
  async [LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    await Vue.axios
      .post<Response<AuthResponse>>('api/Identity/login', payload)
      .then((response) => {
        localStorage.setItem('authResponse', JSON.stringify(response.data.data));
        commit(AUTH_USER, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [REGISTRATION](state, payload: RegistrationRequest): Promise<any> {
    return Vue.axios.post<Response<AuthResponse>>('api/Identity/registration', payload);
    // .then((response) => {
    //   console.log(response);
    //   localStorage.setItem('authResponse', JSON.stringify(response.data.data));
    //   commit(AUTH_USER, response.data.data);
    //   // commit(UPDATE_REG_STEP, 2);
    //   commit(UPDATE_USER_REGISTERED, true);
    // })
    // .catch((err) => {
    //   console.log(err);
    // });
  }
};
