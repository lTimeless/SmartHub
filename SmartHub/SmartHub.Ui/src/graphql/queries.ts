import gql from 'graphql-tag';

// Checks
export const HOME_AND_USERS_EXIST = gql`
  query HomeAndUsersExist {
    applicationIsActive
    usersExist
  }
`;
export const ApplicationIsActive = gql`
  query ApplicationIsActive {
    applicationIsActive
  }
`;
export const UsersExist = gql`
  query UsersExist {
    usersExist
  }
`;
