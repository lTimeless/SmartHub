import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type CreateGroupMutationVariables = Types.Exact<{
  input: Types.CreateGroupInput;
}>;

export type CreateGroupMutation = {
  __typename?: 'AppMutations';
  createGroup: {
    __typename?: 'GroupPayload';
    group?: Types.Maybe<{ __typename?: 'Group'; id: string }>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
  };
};

export const CreateGroupDocument = gql`
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

export function useCreateGroupMutation() {
  return Urql.useMutation<CreateGroupMutation, CreateGroupMutationVariables>(CreateGroupDocument);
}
