import gql from 'graphql-tag';

// App

// Group

// Device

// User

// Identity
export const login = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      token
      user {
        userName
        id
      }
      errors {
        message
        code
      }
      message
    }
  }
`;
export const registration = gql`
  mutation Registration($input: RegistrationInput!) {
    registration(input: $input) {
      token
      user {
        userName
        id
      }
      errors {
        message
        code
      }
      message
    }
  }
`;
