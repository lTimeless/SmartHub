import gql from 'graphql-tag';

export const CREATE_GROUP = gql`
  mutation CreateGroup($input: CreateGroupInput!) {
    createGroup(input: $input) {
      group {
        id
      }
      errors {
        message
        code
      }
    }
  }
`;
export const UPDATE_GROUP = gql`
  mutation UPDATE_GROUP($input: UpdateGroupInput!) {
    updateGroup(input: $input) {
      errors {
        message
        code
      }
    }
  }
`;
