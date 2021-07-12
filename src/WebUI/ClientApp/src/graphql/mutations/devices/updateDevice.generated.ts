import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type Update_DeviceMutationVariables = Types.Exact<{
  input: Types.UpdateDeviceInput;
}>;

export type Update_DeviceMutation = {
  __typename?: 'AppMutations';
  updateDevice: {
    __typename?: 'DevicePayload';
    device?: Types.Maybe<{ __typename?: 'Device'; id: string }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const Update_DeviceDocument = gql`
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

export function useUpdate_DeviceMutation() {
  return Urql.useMutation<Update_DeviceMutation, Update_DeviceMutationVariables>(Update_DeviceDocument);
}
