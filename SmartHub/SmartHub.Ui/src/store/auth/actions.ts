import { ActionContext, ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, ServerResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeAuthResponse, storeToken } from '@/services/auth/authService';
import axiosInstance from '@/router/axios/axios';
import { AUTH_USER, AuthMutations, UPDATE_SIGNIN_BTN } from '@/store/auth/mutations';

// Keys
export const LOGIN = 'LOGIN';
export const REGISTRATION = 'REGISTRATION';
export const LOGOUT = 'LOGOUT';

// Actions
type AugmentedActionContext = {
  commit<K extends keyof AuthMutations>(key: K, payload: Parameters<AuthMutations[K]>[1]): ReturnType<AuthMutations[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

// Action Interface
export interface AuthActions {
  [LOGIN]({ commit }: AugmentedActionContext, payload: LoginRequest): Promise<void>;
  [REGISTRATION](state: AugmentedActionContext, payload: RegistrationRequest): Promise<any>;
  [LOGOUT]({ commit }: AugmentedActionContext): void;
}

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  async [LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    commit(UPDATE_SIGNIN_BTN, undefined);
    await axiosInstance
      .post<ServerResponse<AuthResponse>>('api/Identity/login', payload)
      .then((response) => {
        const authResponse = response.data.data as AuthResponse;
        storeToken(authResponse.token);
        storeAuthResponse(authResponse);
        commit(AUTH_USER, authResponse);
      })
      .catch((err) => {
        console.log(err);
        commit(UPDATE_SIGNIN_BTN, undefined);
      });
  },
  async [REGISTRATION](state, payload: RegistrationRequest): Promise<any> {
    return axiosInstance.post<ServerResponse<AuthResponse>>('api/Identity/registration', payload);
  },
  async [LOGOUT]({ commit }) {
    commit(UPDATE_SIGNIN_BTN, undefined);
  }
};
