import { UpdateUserInput } from '@/types/graphql/inputs';
import { UserPayload } from '@/types/graphql/payloads';
import gql from 'graphql-tag';

/** Variables */
export type UpdateUserMutationVariables = {
  input: UpdateUserInput;
};

/** Payload/Response */
export type UpdateUserMutationPayload = { __typename?: 'AppMutations' } & {
  updateUser: UserPayload;
};

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
