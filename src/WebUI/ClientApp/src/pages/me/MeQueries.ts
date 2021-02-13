import { IdentityPayload } from '@/types/graphql/payloads';
import gql from 'graphql-tag';

export const FRAGMENT_ME = gql`
  fragment IdUserName on User {
    id
    userName
  }
`;

/** Types */
export type MeQueryType = { __typename?: 'AppQueries' } & {
  me: IdentityPayload;
};

export const ME = gql`
  query GetMe {
    me {
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
  ${FRAGMENT_ME}
`;
