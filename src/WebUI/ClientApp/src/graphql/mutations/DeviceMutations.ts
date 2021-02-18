import { gql } from '@urql/core';

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
