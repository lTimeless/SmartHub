import gql from 'graphql-tag';

export const WHO_AM_I = gql`
  query GetMe {
    me {
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
`;
