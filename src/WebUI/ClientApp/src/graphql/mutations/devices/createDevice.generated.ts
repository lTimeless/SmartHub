import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type CreateDeviceMutationVariables = Types.Exact<{
  input: Types.CreateDeviceInput;
}>;

export type CreateDeviceMutation = {
  __typename?: 'AppMutations';
  createDevice: {
    __typename?: 'DevicePayload';
    device?: Types.Maybe<{ __typename?: 'Device'; id: string }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const CreateDeviceDocument = gql`
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

export function useCreateDeviceMutation() {
  return Urql.useMutation<CreateDeviceMutation, CreateDeviceMutationVariables>(CreateDeviceDocument);
}
