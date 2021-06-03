import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetGroupsCountQueryVariables = Types.Exact<{ [key: string]: never }>;

export type GetGroupsCountQuery = { __typename?: 'AppQueries'; groupsCount: number };

export const GetGroupsCountDocument = gql`
  query GetGroupsCount {
    groupsCount
  }
`;

export function useGetGroupsCountQuery(
  options: Omit<Urql.UseQueryArgs<GetGroupsCountQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<GetGroupsCountQuery>({ query: GetGroupsCountDocument, ...options });
}
