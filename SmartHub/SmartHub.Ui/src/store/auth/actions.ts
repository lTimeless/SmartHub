import { ActionContext, ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, ServerResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeAuthResponse } from '@/services/auth/authService';
import axiosInstance from '@/router/axios/axios';
import { AuthMutations, M_AUTH_USER } from '@/store/auth/mutations';

// Keys
export const A_LOGIN = 'A_LOGIN';
export const A_REGISTRATION = 'A_REGISTRATION';
export const A_LOGOUT = 'A_LOGOUT';

// Actions
type AugmentedActionContext = {
  commit<K extends keyof AuthMutations>(key: K, payload?: Parameters<AuthMutations[K]>[1]): ReturnType<AuthMutations[K]>;
} & Omit<ActionContext<AuthState, RootState>, 'commit'>;

// Action Interface
export interface AuthActions {
  [A_LOGIN]({ commit }: AugmentedActionContext, payload: LoginRequest): Promise<void>;
  [A_REGISTRATION](state: AugmentedActionContext, payload: RegistrationRequest): Promise<void>;
  [A_LOGOUT]({ commit }: AugmentedActionContext): void;
}

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  async [A_LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    await axiosInstance
      .post<ServerResponse<AuthResponse>>('api/Identity/login', payload)
      .then((response) => {
        const authResponse = response.data.data as AuthResponse;
        storeAuthResponse(authResponse);
        commit(M_AUTH_USER, authResponse);
      })
      .catch((err) => Promise.reject(err));
  },
  async [A_REGISTRATION](state, payload: RegistrationRequest): Promise<void> {
    return axiosInstance
      .post<ServerResponse<AuthResponse>>('api/Identity/registration', payload)
      .then(async (response) => {
        const auth = response.data.data as AuthResponse;
        await storeAuthResponse(auth);
        await state.commit(M_AUTH_USER, auth);
      })
      .catch((err) => Promise.reject(err));
  },
  async [A_LOGOUT]({ commit }) {
    console.log('logout');
  }
};
