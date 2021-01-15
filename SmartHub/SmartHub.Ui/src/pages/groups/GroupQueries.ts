import gql from 'graphql-tag';

export const GET_GROUPS = gql`
  query GetGroups {
    groups {
      id
      name
      createdBy
      lastModifiedBy
      lastModifiedAt
      createdAt
      description
      devices {
        id
        name
        pluginName
        pluginName
      }
    }
  }
`;

export const GET_GROUP_BY_ID = gql`
  query GET_GROUP_BY_ID($name: String!) {
    groups(where: { name: { eq: $name } }) {
      id
      name
      createdBy
      lastModifiedBy
      lastModifiedAt
      createdAt
      description
      devices {
        id
        name
        pluginName
        pluginName
      }
    }
  }
`;
