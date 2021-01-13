import gql from 'graphql-tag';

export const REGISTRATION = gql`
  mutation Registration($input: RegistrationInput!) {
    registration(input: $input) {
      token
      user {
        id
        userName
      }
      errors {
        message
        code
      }
      message
    }
  }
`;
