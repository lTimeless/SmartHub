import gql from 'graphql-tag';

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
