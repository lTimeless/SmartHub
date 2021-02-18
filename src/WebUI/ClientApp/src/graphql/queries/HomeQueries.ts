import gql from 'graphql-tag';

/** Types */
export type GetDevicesCountQueryType = { __typename?: 'AppQueries' } & {
  devicesCount: number;
};

export type GetGroupsCountQueryType = { __typename?: 'AppQueries' } & {
  groupsCount: number;
};

/** Queries */
export const GET_DEVICES_COUNT = gql`
  query GetDevicesCount {
    devicesCount
  }
`;

export const GET_GROUPS_COUNT = gql`
  query GetGroupsCount {
    groupsCount
  }
`;
