import * as Types from '../graphql.types';

import gql from 'graphql-tag';
export type IdUserNameFragment = {
  __typename?: 'User';
  id?: Types.Maybe<string>;
  userName?: Types.Maybe<string>;
};

export const IdUserNameFragmentDoc = gql`
  fragment IdUserName on User {
    id
    userName
  }
`;
