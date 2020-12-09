import gql from 'graphql-tag';

// App

// Group

// Device

// User
export const UPDATE_USER = gql`
  mutation Updateuser($input: UpdateUserInput!) {
    updateUser(input: $input) {
      user {
        id
        userName
        createdAt
        createdBy
        lastModifiedAt
        lastModifiedBy
        phoneNumber
        personInfo
        email
        personName {
          firstName
          lastName
          middleName
        }
      }
    }
  }
`;
// Identity
export const LOGIN = gql`
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
export const REGISTRATION = gql`
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
