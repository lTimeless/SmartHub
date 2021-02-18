import { RegistrationInput } from '@/types/graphql/inputs';
import { IdentityPayload } from '@/types/graphql/payloads';
import gql from 'graphql-tag';

/** Variables */
export type RegistrationMutationVariables = {
  input: RegistrationInput;
};

/** Payload/Response */
export type RegistrationMutationPayload = { __typename?: 'AppMutations' } & {
  registration: IdentityPayload;
};

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
