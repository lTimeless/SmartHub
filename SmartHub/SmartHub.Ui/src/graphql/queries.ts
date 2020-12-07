import gql from 'graphql-tag';

export const checkApp = gql`
  {
    checkApp {
      data
      success
      message
    }
  }
`;

export const checkUsers = gql`
  {
    checkUsers {
      data
      success
      message
    }
  }
`;
