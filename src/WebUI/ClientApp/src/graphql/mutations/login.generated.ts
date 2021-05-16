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
    message?: Types.Maybe<string>;
    user?: Types.Maybe<{
      __typename?: 'User';
      id?: Types.Maybe<string>;
      userName?: Types.Maybe<string>;
      isFirstLogin: boolean;
    }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const LoginDocument = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      user {
        id
        userName
        isFirstLogin
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
