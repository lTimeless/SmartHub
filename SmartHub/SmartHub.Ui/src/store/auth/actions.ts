import { ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, ServerResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { AUTH_USER, UPDATE_SIGNIN_BTN } from '@/store/auth/mutations';
import { storeAuthResponse, storeToken } from '@/services/auth/authService';
import axiosInstance from '@/router/axios/axios';

// ActionType keys
export const LOGIN = 'LOGIN';
export const LOGOUT = 'LOGOUT';
export const REGISTRATION = 'REGISTRATION';

export const actions: ActionTree<AuthState, RootState> = {
  async [LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    commit(UPDATE_SIGNIN_BTN);
    await axiosInstance
      .post<ServerResponse<AuthResponse>>('api/Identity/login', payload)
      .then((response) => {
        const authResponse = response.data.data as AuthResponse;
        storeToken(authResponse.token);
        storeAuthResponse(authResponse);
        commit(AUTH_USER, response.data.data);
      })
      .catch((err) => {
        console.log(err);
        commit(UPDATE_SIGNIN_BTN);
      });
  },
  async [REGISTRATION](state, payload: RegistrationRequest): Promise<any> {
    return axiosInstance.post<ServerResponse<AuthResponse>>('api/Identity/registration', payload);
  },
  async [LOGOUT]({ commit }) {
    commit(UPDATE_SIGNIN_BTN);
  }
};
