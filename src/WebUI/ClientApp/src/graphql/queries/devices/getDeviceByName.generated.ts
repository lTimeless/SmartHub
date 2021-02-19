import * as Types from '../../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetDeviceByNameQueryVariables = Types.Exact<{
  name: Types.Scalars['String'];
}>;

export type GetDeviceByNameQuery = {
  __typename?: 'AppQueries';
  devices: Array<{
    __typename?: 'Device';
    id: string;
    name: string;
    description?: Types.Maybe<string>;
    primaryConnection: Types.ConnectionTypes;
    secondaryConnection: Types.ConnectionTypes;
    createdAt: any;
    lastModifiedAt: any;
    createdBy: string;
    lastModifiedBy: string;
    pluginName: string;
    pluginTypes: Types.PluginTypes;
    ip: { __typename?: 'IpAddress'; ipv4: string };
    company: { __typename?: 'Company'; name: string; shortName: string };
  }>;
};

export const GetDeviceByNameDocument = gql`
  query GetDeviceByName($name: String!) {
    devices(where: { name: { eq: $name } }) {
      id
      name
      description
      primaryConnection
      secondaryConnection
      createdAt
      lastModifiedAt
      createdBy
      lastModifiedBy
      pluginName
      pluginTypes
      ip {
        ipv4
      }
      company {
        name
        shortName
      }
    }
  }
`;

export function useGetDeviceByNameQuery(
  options: Omit<Urql.UseQueryArgs<GetDeviceByNameQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<GetDeviceByNameQuery>({ query: GetDeviceByNameDocument, ...options });
}
