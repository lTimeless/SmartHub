import * as Types from '../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type RegistrationMutationVariables = Types.Exact<{
  input: Types.RegistrationInput;
}>;

export type RegistrationMutation = {
  __typename?: 'AppMutations';
  registration: {
    __typename?: 'IdentityPayload';
    token?: Types.Maybe<string>;
    message?: Types.Maybe<string>;
    user?: Types.Maybe<{ __typename?: 'User'; id?: Types.Maybe<string>; userName?: Types.Maybe<string> }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const RegistrationDocument = gql`
  mutation Registration($input: RegistrationInput!) {
    registration(input: $input) {
      token
      user {
        id
        userName
      }
      errors {
        message
        code
      }
      message
    }
  }
`;

export function useRegistrationMutation() {
  return Urql.useMutation<RegistrationMutation, RegistrationMutationVariables>(RegistrationDocument);
}
