import gql from 'graphql-tag';

export const FRAGMENT_ME = gql`
  fragment IdUserName on User {
    id
    userName
  }
`;
