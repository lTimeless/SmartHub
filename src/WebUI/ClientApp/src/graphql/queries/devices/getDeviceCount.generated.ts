import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetDevicesCountQueryVariables = Types.Exact<{ [key: string]: never }>;

export type GetDevicesCountQuery = { __typename?: 'AppQueries'; devicesCount: number };

export const GetDevicesCountDocument = gql`
  query GetDevicesCount {
    devicesCount
  }
`;

export function useGetDevicesCountQuery(
  options: Omit<Urql.UseQueryArgs<GetDevicesCountQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<GetDevicesCountQuery>({ query: GetDevicesCountDocument, ...options });
}
