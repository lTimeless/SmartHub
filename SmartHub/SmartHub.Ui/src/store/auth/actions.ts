import { ActionContext, ActionTree } from 'vuex';
import { IdentityPayload, RegistrationInput, UpdateUserInput } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeToken } from '@/services/auth/authService';
import { AuthMutations, AuthMutationTypes } from '@/store/auth/mutations';
import { WHO_AM_I } from '@/graphql/queries';
import { apolloClient } from '@/apollo';
import { LOGIN, REGISTRATION, UPDATE_USER } from '@/graphql/mutations';

// Keys
export enum AuthActionTypes {
  // Me
  ME = 'ME',
  UPDATE_ME = 'UPDATE_ME',
  // Identity
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
  // Me
  [AuthActionTypes.ME]({ commit }: ActionAugments): Promise<void>;
  [AuthActionTypes.UPDATE_ME]({ commit }: ActionAugments, payload: UpdateUserInput): Promise<void>;
  // Identity
  [AuthActionTypes.LOGIN]({ commit }: ActionAugments, payload: IdentityPayload): Promise<void>;
  [AuthActionTypes.REGISTRATION]({ commit }: ActionAugments, payload: RegistrationInput): Promise<void>;
  [AuthActionTypes.LOGOUT]({ commit }: ActionAugments): void;
};

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  // User
  async [AuthActionTypes.ME]({ commit }): Promise<void> {
    const { data } = await apolloClient.query({ query: WHO_AM_I });
    commit(AuthMutationTypes.UPDATE_ME, data.me);
  },
  async [AuthActionTypes.UPDATE_ME]({ commit }, payload: UpdateUserInput): Promise<void> {
    await apolloClient
      .mutate({ mutation: UPDATE_USER, variables: { input: payload } })
      .then((res) => {
        commit(AuthMutationTypes.UPDATE_ME, res.data.user);
        return Promise.resolve();
      })
      .catch((err) => Promise.reject(err));
  },
  // Identity
  async [AuthActionTypes.LOGIN]({ commit }, payload: IdentityPayload): Promise<void> {
    await apolloClient
      .mutate({ mutation: LOGIN, variables: { input: payload } })
      .then((res) => {
        if (res.data.login.token != null) {
          storeToken(res.data.login.token);
          commit(AuthMutationTypes.UPDATE_TOKEN, res.data.login.token);
          return Promise.resolve();
        }
        return Promise.reject();
      })
      .catch((err) => Promise.reject(err));
  },
  async [AuthActionTypes.REGISTRATION]({ commit }, payload: RegistrationInput): Promise<void> {
    await apolloClient
      .mutate({ mutation: REGISTRATION, variables: { input: payload } })
      .then((res) => {
        if (res.data.registration.token != null) {
          storeToken(res.data.registration.token);
          commit(AuthMutationTypes.UPDATE_TOKEN, res.data.registration.token);
          return Promise.resolve();
        }
        return Promise.reject();
      })
      .catch((err) => Promise.reject(err));
  },
  async [AuthActionTypes.LOGOUT]() {
    console.log('logout');
  }
};
