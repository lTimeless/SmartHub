import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type UpdateGroupMutationVariables = Types.Exact<{
  input: Types.UpdateGroupInput;
}>;

export type UpdateGroupMutation = {
  __typename?: 'AppMutations';
  updateGroup: {
    __typename?: 'GroupPayload';
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const UpdateGroupDocument = gql`
  mutation UpdateGroup($input: UpdateGroupInput!) {
    updateGroup(input: $input) {
      errors {
        message
        code
      }
    }
  }
`;

export function useUpdateGroupMutation() {
  return Urql.useMutation<UpdateGroupMutation, UpdateGroupMutationVariables>(UpdateGroupDocument);
}
