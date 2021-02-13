import { LoginInput } from '@/types/graphql/inputs';
import { IdentityPayload } from '@/types/graphql/payloads';
import gql from 'graphql-tag';

/** Variables */
export type LoginMutationVariables = {
  input: LoginInput;
};

/** Payload/Response */
export type LoginMutationPayload = { __typename?: 'AppMutations' } & {
  login: IdentityPayload;
};

/** Gql document */
export const LOGIN = gql`
  mutation Login($input: LoginInput!) {
    login(input: $input) {
      token
      user {
        id
        userName
        isFirstLogin
      }
      errors {
        message
        code
      }
      message
    }
  }
`;
