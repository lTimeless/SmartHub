import gql from 'graphql-tag';

// Group
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

// Device
export const CREATE_DEVICE = gql`
  mutation CreateDevice($input: CreateDeviceInput!) {
    createDevice(input: $input) {
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

