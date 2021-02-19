import * as Types from '../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type InitAppMutationVariables = Types.Exact<{
  input: Types.AppConfigInitInput;
}>;

export type InitAppMutation = {
  __typename?: 'AppMutations';
  initializeApp: {
    __typename?: 'InitPayload';
    message?: Types.Maybe<string>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
    appConfig?: Types.Maybe<{ __typename?: 'AppConfig'; applicationName?: Types.Maybe<string> }>;
  };
};

export const InitAppDocument = gql`
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

export function useInitAppMutation() {
  return Urql.useMutation<InitAppMutation, InitAppMutationVariables>(InitAppDocument);
}
