import { ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, Response } from '@/types/types';
import Vue from 'vue';
import { AUTH_USER } from '@/store/user/mutations';
import { RootState, UserState } from '@/store/index.types';

// ActionType keys
export const LOGIN = 'LOGIN';
export const REGISTRATION = 'REGISTRATION';

export const actions: ActionTree<UserState, RootState> = {
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
  async [REGISTRATION]({ commit }, payload: RegistrationRequest): Promise<void> {
    await Vue.axios
      .post<Response<AuthResponse>>('api/Identity/registration', payload)
      .then((response) => {
        console.log(response);
        localStorage.setItem('authResponse', JSON.stringify(response.data.data));
        commit(AUTH_USER, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }
};
