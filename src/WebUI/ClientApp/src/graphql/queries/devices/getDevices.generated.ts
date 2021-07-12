import * as Types from '../../graphql.types';

import { DeviceStatusFragment } from '../../fragments/deviceStatus.generated';
import gql from 'graphql-tag';
import { DeviceStatusFragmentDoc } from '../../fragments/deviceStatus.generated';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetDevicesQueryVariables = Types.Exact<{ [key: string]: never }>;

export type GetDevicesQuery = {
  __typename?: 'AppQueries';
  devices: Array<
    {
      __typename?: 'Device';
      id: string;
      name: string;
      description?: Types.Maybe<string>;
      primaryConnection: Types.ConnectionTypes;
      secondaryConnection: Types.ConnectionTypes;
      pluginName: string;
      pluginTypes: Types.PluginTypes;
      ip: { __typename?: 'IpAddress'; ipv4: string };
      company: { __typename?: 'Company'; name: string; shortName: string };
    } & DeviceStatusFragment
  >;
};

export const GetDevicesDocument = gql`
  query GetDevices {
    devices {
      id
      name
      description
      primaryConnection
      secondaryConnection
      pluginName
      pluginTypes
      ip {
        ipv4
      }
      company {
        name
        shortName
      }
      ...DeviceStatus
    }
  }
  ${DeviceStatusFragmentDoc}
`;

export function useGetDevicesQuery(options: Omit<Urql.UseQueryArgs<GetDevicesQueryVariables>, 'query'> = {}) {
  return Urql.useQuery<GetDevicesQuery>({ query: GetDevicesDocument, ...options });
}
