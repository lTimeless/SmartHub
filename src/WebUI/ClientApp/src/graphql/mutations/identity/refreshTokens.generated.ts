import * as Types from '../../graphql.types';

import { IdentityFragment } from './identity.fragment.generated';
import gql from 'graphql-tag';
import { IdentityFragmentDoc } from './identity.fragment.generated';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type RefreshTokensMutationVariables = Types.Exact<{ [key: string]: never }>;

export type RefreshTokensMutation = {
  __typename?: 'AppMutations';
  refreshTokens: {
    __typename?: 'IdentityPayload';
    message?: Types.Maybe<string>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  } & IdentityFragment;
};

export const RefreshTokensDocument = gql`
  mutation RefreshTokens {
    refreshTokens {
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

export function useRefreshTokensMutation() {
  return Urql.useMutation<RefreshTokensMutation, RefreshTokensMutationVariables>(RefreshTokensDocument);
}
