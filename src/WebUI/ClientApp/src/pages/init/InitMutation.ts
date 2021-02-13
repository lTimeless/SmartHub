import { AppConfigInitInput } from '@/types/graphql/inputs';
import { InitPayload } from '@/types/graphql/payloads';
import gql from 'graphql-tag';

/** Variables */
export type InitMutationVariables = {
  input: AppConfigInitInput;
};

/** Payload/Response */
export type InitMutationPayload = { __typename?: 'AppMutations' } & {
  initializeApp: InitPayload;
};

export const INITIALIZE_APP = gql`
  mutation InitApp($input: AppConfigInitInput!) {
    initializeApp(input: $input) {
      errors {
        message
        code
      }
      appConfig {
        applicationName
      }
      message
    }
  }
`;
