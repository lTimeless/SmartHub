import gql from 'graphql-tag';

export const GET_DEVICE_BY_ID = gql`
  query GET_DEVICE_BY_ID($id: String!) {
    devices(where: { id: { eq: $id } }) {
      id
      name
      description
      primaryConnection
      secondaryConnection
      createdAt
      lastModifiedAt
      createdBy
      lastModifiedBy
      pluginName
      pluginTypes
      ip {
        ipv4
      }
      company {
        name
        shortName
      }
    }
  }
`;

export const GET_DEVICES = gql`
  query GetDevices {
    devices {
      id
      name
      description
      primaryConnection
      secondaryConnection
      createdAt
      lastModifiedAt
      createdBy
      lastModifiedBy
      pluginName
      pluginTypes
      ip {
        ipv4
      }
      company {
        name
        shortName
      }
    }
  }
`;
