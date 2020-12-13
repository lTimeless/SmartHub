import gql from 'graphql-tag';

// App
export const INITIALIZE_APP = gql`
  mutation InitApp($input: AppConfigInitInput!) {
    initializeApp(input: $input) {
      errors {
        message
        code
      }
      appConfig {
        applicationName
      }
      message
    }
  }
`;
// Group
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

// Device
export const UPDATE_DEVICE = gql`
  mutation UPDATE_DEVICE($input: UpdateDeviceInput!) {
    updateDevice(input: $input) {
      device {
        id
      }
      errors {
        message
        code
      }
    }
  }
`;
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
