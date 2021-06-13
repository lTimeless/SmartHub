import * as Types from '../../graphql.types';

import { IdentityFragment } from './identity.fragment.generated';
import gql from 'graphql-tag';
import { IdentityFragmentDoc } from './identity.fragment.generated';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type RegistrationMutationVariables = Types.Exact<{
  input: Types.RegistrationInput;
}>;

export type RegistrationMutation = {
  __typename?: 'AppMutations';
  registration: {
    __typename?: 'IdentityPayload';
    message?: Types.Maybe<string>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  } & IdentityFragment;
};

export const RegistrationDocument = gql`
  mutation Registration($input: RegistrationInput!) {
    registration(input: $input) {
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

export function useRegistrationMutation() {
  return Urql.useMutation<RegistrationMutation, RegistrationMutationVariables>(RegistrationDocument);
}
