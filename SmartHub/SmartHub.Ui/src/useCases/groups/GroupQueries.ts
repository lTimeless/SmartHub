import gql from 'graphql-tag';

export const GET_GROUPS = gql`
  query GetGroups {
    groups {
      id
      name
      isSubGroup
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
      subGroups {
        id
        name
      }
    }
  }
`;

export const GET_GROUP_BY_ID = gql`
  query GET_GROUP_BY_ID($id: String!) {
    groups(where: { id: { eq: $id } }) {
      id
      name
      isSubGroup
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
      subGroups {
        id
        name
      }
    }
  }
`;
