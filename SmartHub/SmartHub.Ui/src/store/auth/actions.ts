import { ActionContext, ActionTree } from 'vuex';
import { IdentityPayload, RegistrationInput, UpdateUserInput, User } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeToken } from '@/services/auth/authService';
import { AuthMutations, AuthMutationTypes } from '@/store/auth/mutations';
import { useQuery, useResult } from '@vue/apollo-composable';
import { WHO_AM_I } from '@/graphql/queries';
import { apolloClient } from '@/apollo';
import { LOGIN, UPDATE_USER } from '@/graphql/mutations';

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
  [AuthActionTypes.ME]({ commit }: ActionAugments): Promise<void>;
  [AuthActionTypes.LOGIN]({ commit }: ActionAugments, payload: IdentityPayload): Promise<void>;
  [AuthActionTypes.REGISTRATION](state: ActionAugments, payload: RegistrationInput): Promise<void>;
  [AuthActionTypes.LOGOUT]({ commit }: ActionAugments): void;
};

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  // User
  async [AuthActionTypes.ME]({ commit }): Promise<void> {
    const { result } = useQuery(WHO_AM_I);
    const user = useResult<User>(result);
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-ignore
    commit(AuthMutationTypes.UPDATE_ME, user);
  },

  async [AuthActionTypes.UPDATE_ME]({ commit }, payload: UpdateUserInput): Promise<void> {
    await apolloClient.mutate({ mutation: UPDATE_USER, variables: { input: payload } }).then((res) => {
      commit(AuthActionTypes.UPDATE_ME, res.data.user);
    });
  },
  // Identity
  async [AuthActionTypes.LOGIN]({ commit }, payload: IdentityPayload): Promise<void> {
    await apolloClient.mutate({ mutation: LOGIN, variables: { input: payload } }).then((res) => {
      if (res.data.login.token != null) {
        storeToken(res.data.login.token);
      }
      commit(AuthMutationTypes.UPDATE_TOKEN, res.data.login.token);
    });
  },
  async [AuthActionTypes.REGISTRATION](state, payload: RegistrationInput): Promise<void> {
    // await postRegistration(payload)
    //   .then((response) => {
    //     if (!response.success) {
    //       return Promise.reject(response.message);
    //     }
    //     storeAuthResponse(response.data as IdentityPayload);
    //     state.commit(AuthMutationTypes.TOKEN, response.data as IdentityPayload);
    //     return Promise.resolve();
    //   })
    //   .catch((err) => Promise.reject(err));
  },
  async [AuthActionTypes.LOGOUT]() {
    console.log('logout');
  }
};
