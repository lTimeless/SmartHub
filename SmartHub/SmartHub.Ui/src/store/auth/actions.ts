import { ActionContext, ActionTree } from 'vuex';
import { IdentityPayload, RegistrationInput, UpdateUserInput, User } from '@/types/types';
import { RootState, AuthState } from '@/store/index.types';
import { storeToken } from '@/services/auth/authService';
import { AuthMutations, AuthMutationTypes } from '@/store/auth/mutations';
// import { postLogin, postRegistration, getMe, putMe } from '@/services/apis/identity';
import { useQuery, useResult, useApolloClient } from '@vue/apollo-composable';
import { whoAmI } from '@/graphql/queries';

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
  [AuthActionTypes.UPDATE_ME]({ commit }: ActionAugments, payload: UpdateUserInput): Promise<void>;
  [AuthActionTypes.LOGIN]({ commit }: ActionAugments, payload: IdentityPayload): Promise<void>;
  [AuthActionTypes.REGISTRATION](state: ActionAugments, payload: RegistrationInput): Promise<void>;
  [AuthActionTypes.LOGOUT]({ commit }: ActionAugments): void;
};

// Define Actions
export const actions: ActionTree<AuthState, RootState> & AuthActions = {
  async [AuthActionTypes.ME]({ commit }): Promise<void> {
    const { result } = useQuery(whoAmI);
    const user = useResult<User>(result);
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-ignore
    commit(AuthMutationTypes.User, user);
  },
  async [AuthActionTypes.UPDATE_ME]({ commit }, payload: UpdateUserInput): Promise<void> {
    // await putMe(payload)
    //   .then((response) => {
    //     if (!response.success) {
    //       return Promise.reject(response.message);
    //     }
    //     commit(AuthMutationTypes.ME, response.data as User);
    //     return Promise.resolve(response.data);
    //   })
    //   .catch((err) => Promise.reject(err));
    const { client } = useApolloClient();
    await client.mutate({ mutation: whoAmI, variables: payload });
  },
  async [AuthActionTypes.LOGIN]({ commit }, payload: IdentityPayload): Promise<void> {
    if (payload.token != null) {
      storeToken(payload.token);
    }
    commit(AuthMutationTypes.User, payload.user);
    commit(AuthMutationTypes.TOKEN, payload.token);
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
