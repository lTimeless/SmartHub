import gql from 'graphql-tag';

export const LOGIN = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      token
      user {
        id
        userName
        isFirstLogin
      }
      errors {
        message
        code
      }
      message
    }
  }
`;
