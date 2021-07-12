import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetGroupsQueryVariables = Types.Exact<{ [key: string]: never }>;

export type GetGroupsQuery = {
  __typename?: 'AppQueries';
  groups: Array<{
    __typename?: 'Group';
    id: string;
    name: string;
    createdBy: string;
    lastModifiedBy: string;
    lastModifiedAt: any;
    createdAt: any;
    description?: Types.Maybe<string>;
    devices: Array<{ __typename?: 'Device'; id: string; name: string; pluginName: string }>;
  }>;
};

export const GetGroupsDocument = gql`
  query GetGroups {
    groups {
      id
      name
      createdBy
      lastModifiedBy
      lastModifiedAt
      createdAt
      description
      devices {
        id
        name
        pluginName
        pluginName
      }
    }
  }
`;

export function useGetGroupsQuery(options: Omit<Urql.UseQueryArgs<GetGroupsQueryVariables>, 'query'> = {}) {
  return Urql.useQuery<GetGroupsQuery>({ query: GetGroupsDocument, ...options });
}
