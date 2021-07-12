import * as Types from '../../graphql.types';

import {IdentityFragment, IdentityFragmentDoc} from './identity.fragment.generated';
import gql from 'graphql-tag';
import * as Urql from '@urql/vue';

export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type LogoutQueryVariables = Types.Exact<{ [key: string]: never }>;

export type LogoutQuery = {
  __typename?: 'AppQueries';
  logout: {
    __typename?: 'IdentityPayload';
    message?: Types.Maybe<string>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  } & IdentityFragment;
};

export const LogoutDocument = gql`
  query Logout {
    logout {
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

export function useLogoutQuery(options: Omit<Urql.UseQueryArgs<LogoutQueryVariables>, 'query'> = {}) {
  return Urql.useQuery<LogoutQuery>({query: LogoutDocument, ...options});
}
