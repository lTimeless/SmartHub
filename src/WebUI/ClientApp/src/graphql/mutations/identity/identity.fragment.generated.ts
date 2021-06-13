import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
export type IdentityFragment = {
  __typename?: 'IdentityPayload';
  isAuthenticated: boolean;
  user?: Types.Maybe<{
    __typename?: 'User';
    id?: Types.Maybe<string>;
    userName?: Types.Maybe<string>;
    isFirstLogin: boolean;
    roles: Array<string>;
  }>;
};

export const IdentityFragmentDoc = gql`
  fragment Identity on IdentityPayload {
    isAuthenticated
    user {
      id
      userName
      isFirstLogin
      roles
    }
  }
`;
