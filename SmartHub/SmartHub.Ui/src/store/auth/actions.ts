import { ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, ServerResponse } from '@/types/types';
import Vue from 'vue';
import { RootState, AuthState } from '@/store/index.types';
import { AUTH_USER } from '@/store/auth/mutations';
import { storeAuthResponse, storeToken } from '@/services/auth/authService';
import axios from 'axios';

// ActionType keys
export const LOGIN = 'LOGIN';
export const REGISTRATION = 'REGISTRATION';

export const actions: ActionTree<AuthState, RootState> = {
  async [LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    await axios
      .post<ServerResponse<AuthResponse>>('api/Identity/login', payload)
      .then((response) => {
        const authResponse = response.data.data as AuthResponse;
        storeToken(authResponse.token);
        storeAuthResponse(authResponse);
        commit(AUTH_USER, response.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  async [REGISTRATION](state, payload: RegistrationRequest): Promise<any> {
    return axios.post<ServerResponse<AuthResponse>>('api/Identity/registration', payload);
  }
};
