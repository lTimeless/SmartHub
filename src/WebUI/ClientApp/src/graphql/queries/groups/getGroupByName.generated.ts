import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetGroupByNameQueryVariables = Types.Exact<{
  name: Types.Scalars['String'];
}>;

export type GetGroupByNameQuery = {
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

export const GetGroupByNameDocument = gql`
  query GetGroupByName($name: String!) {
    groups(where: { name: { eq: $name } }) {
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

export function useGetGroupByNameQuery(
  options: Omit<Urql.UseQueryArgs<GetGroupByNameQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<GetGroupByNameQuery>({ query: GetGroupByNameDocument, ...options });
}
