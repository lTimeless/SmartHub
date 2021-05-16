import * as Types from '../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type LoginMutationVariables = Types.Exact<{
  input: Types.LoginInput;
}>;

export type LoginMutation = {
  __typename?: 'AppMutations';
  login: {
    __typename?: 'IdentityPayload';
    isAuthenticated: boolean;
    message?: Types.Maybe<string>;
    user?: Types.Maybe<{
      __typename?: 'User';
      id?: Types.Maybe<string>;
      userName?: Types.Maybe<string>;
      isFirstLogin: boolean;
      roles: Array<string>;
    }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const LoginDocument = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      isAuthenticated
      user {
        id
        userName
        isFirstLogin
        roles
      }
      errors {
        message
        code
      }
      message
    }
  }
`;

export function useLoginMutation() {
  return Urql.useMutation<LoginMutation, LoginMutationVariables>(LoginDocument);
}
