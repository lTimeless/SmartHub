import gql from 'graphql-tag';

/** Types */
export type ApplicationIsActiveQueryType = { __typename?: 'AppQueries' } & {
  applicationIsActive: number;
};

export const APP_IS_ACTIVE = gql`
  query ApplicationIsActive {
    applicationIsActive
  }
`;
