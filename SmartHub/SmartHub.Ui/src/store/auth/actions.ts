import { ActionContext, ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, ServerResponse } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeAuthResponse } from '@/services/auth/authService';
import { AuthMutations, M_AUTH_USER } from '@/store/auth/mutations';
import { postLogin, postRegistration } from '@/services/apis/user.service';

// Keys
export const A_LOGIN = 'A_LOGIN';
export const A_REGISTRATION = 'A_REGISTRATION';
export const A_LOGOUT = 'A_LOGOUT';

// Actions
type AugmentedActionContext = {
  commit<K extends keyof AuthMutations>(key: K, payload: Parameters<AuthMutations[K]>[1] | null): ReturnType<AuthMutations[K]>;
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
    await postLogin(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        storeAuthResponse(response.data as AuthResponse);
        commit(M_AUTH_USER, response.data);
        return Promise.resolve(response.data);
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [A_REGISTRATION](state, payload: RegistrationRequest): Promise<void> {
    await postRegistration(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        storeAuthResponse(response.data as AuthResponse);
        state.commit(M_AUTH_USER, response.data);
        return Promise.resolve();
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [A_LOGOUT]({ commit }) {
    console.log('logout');
  }
};
