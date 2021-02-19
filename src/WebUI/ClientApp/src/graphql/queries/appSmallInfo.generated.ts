import * as Types from '../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type ApplicationIsActiveQueryVariables = Types.Exact<{ [key: string]: never }>;

export type ApplicationIsActiveQuery = { __typename?: 'AppQueries'; applicationIsActive: boolean };

export type HomeAndUsersExistQueryVariables = Types.Exact<{ [key: string]: never }>;

export type HomeAndUsersExistQuery = {
  __typename?: 'AppQueries';
  applicationIsActive: boolean;
  usersExist: boolean;
};

export const ApplicationIsActiveDocument = gql`
  query ApplicationIsActive {
    applicationIsActive
  }
`;

export function useApplicationIsActiveQuery(
  options: Omit<Urql.UseQueryArgs<ApplicationIsActiveQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<ApplicationIsActiveQuery>({ query: ApplicationIsActiveDocument, ...options });
}
export const HomeAndUsersExistDocument = gql`
  query HomeAndUsersExist {
    applicationIsActive
    usersExist
  }
`;

export function useHomeAndUsersExistQuery(
  options: Omit<Urql.UseQueryArgs<HomeAndUsersExistQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<HomeAndUsersExistQuery>({ query: HomeAndUsersExistDocument, ...options });
}
