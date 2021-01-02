import gql from 'graphql-tag';

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
