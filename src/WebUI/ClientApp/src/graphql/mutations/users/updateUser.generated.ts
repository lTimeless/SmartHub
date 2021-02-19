import * as Types from '../../graphql.types';

import { IdUserNameFragment } from '../../fragments/userFragment.generated';
import gql from 'graphql-tag';
import { IdUserNameFragmentDoc } from '../../fragments/userFragment.generated';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type UpdateUserMutationVariables = Types.Exact<{
  input: Types.UpdateUserInput;
}>;

export type UpdateUserMutation = {
  __typename?: 'AppMutations';
  updateUser: {
    __typename?: 'UserPayload';
    user: {
      __typename?: 'User';
      createdAt: any;
      createdBy: string;
      lastModifiedAt: any;
      lastModifiedBy: string;
      phoneNumber?: Types.Maybe<string>;
      personInfo: string;
      email?: Types.Maybe<string>;
      personName: { __typename?: 'PersonName'; firstName: string; lastName: string; middleName: string };
    } & IdUserNameFragment;
  };
};

export const UpdateUserDocument = gql`
  mutation UpdateUser($input: UpdateUserInput!) {
    updateUser(input: $input) {
      user {
        ...IdUserName
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
  ${IdUserNameFragmentDoc}
`;

export function useUpdateUserMutation() {
  return Urql.useMutation<UpdateUserMutation, UpdateUserMutationVariables>(UpdateUserDocument);
}
