import * as Types from '../../graphql.types';

import { IdentityFragment } from './identity.fragment.generated';
import gql from 'graphql-tag';
import { IdentityFragmentDoc } from './identity.fragment.generated';
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
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  } & IdentityFragment;
};

export const LoginDocument = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      ...Identity
      errors {
        message
        code
      }
      message
    }
  }
  ${IdentityFragmentDoc}
`;

export function useLoginMutation() {
  return Urql.useMutation<LoginMutation, LoginMutationVariables>(LoginDocument);
}
