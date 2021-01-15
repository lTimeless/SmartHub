import gql from 'graphql-tag';

export const GET_DEVICES_COUNT = gql`
  query GetDevicesCount {
    devicesCount
  }
`;

export const GET_GROUPS_COUNT = gql`
  query GetGroupsCount {
    parentGroupsCount
    subGroupsCount
  }
`;
