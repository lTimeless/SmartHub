import { ActionContext, ActionTree } from 'vuex';
import { LoginRequest, AuthResponse, RegistrationRequest, UserUpdateRequest, User } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeAuthResponse } from '@/services/auth/authService';
import { AuthMutations, AuthMutationTypes } from '@/store/auth/mutations';
import { postLogin, postRegistration, getMe, putMe } from '@/services/apis/identity.service';

// Keys
export enum AuthActionTypes {
  ME = 'ME',
  UPDATE_ME = 'UPDATE_ME',
  LOGIN = 'LOGIN',
  REGISTRATION = 'REGISTRATION',
  LOGOUT = 'LOGOUT'
}

// Actions
type ActionAugments = Omit<ActionContext<AuthState, RootState>, 'commit'> & {
  commit<K extends keyof AuthMutations>(
    key: K,
    payload: Parameters<AuthMutations[K]>[1]
  ): ReturnType<AuthMutations[K]>;
};

// Action Interface
export type AuthActions = {
  [AuthActionTypes.ME]({ commit }: ActionAugments): Promise<void>;
  [AuthActionTypes.UPDATE_ME]({ commit }: ActionAugments, payload: UserUpdateRequest): Promise<void>;
  [AuthActionTypes.LOGIN]({ commit }: ActionAugments, payload: LoginRequest): Promise<void>;
  [AuthActionTypes.REGISTRATION](state: ActionAugments, payload: RegistrationRequest): Promise<void>;
  [AuthActionTypes.LOGOUT]({ commit }: ActionAugments): void;
};

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  async [AuthActionTypes.ME]({ commit }): Promise<void> {
    await getMe()
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(AuthMutationTypes.ME, response.data as User);
        return Promise.resolve(response.data);
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [AuthActionTypes.UPDATE_ME]({ commit }, payload: UserUpdateRequest): Promise<void> {
    await putMe(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(AuthMutationTypes.ME, response.data as User);
        return Promise.resolve(response.data);
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [AuthActionTypes.LOGIN]({ commit }, payload: LoginRequest): Promise<void> {
    await postLogin(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        storeAuthResponse(response.data as AuthResponse);
        commit(AuthMutationTypes.AUTH, response.data as AuthResponse);
        return Promise.resolve(response.data);
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [AuthActionTypes.REGISTRATION](state, payload: RegistrationRequest): Promise<void> {
    await postRegistration(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        storeAuthResponse(response.data as AuthResponse);
        state.commit(AuthMutationTypes.AUTH, response.data as AuthResponse);
        return Promise.resolve();
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });
  },
  async [AuthActionTypes.LOGOUT]() {
    console.log('logout');
  }
};
