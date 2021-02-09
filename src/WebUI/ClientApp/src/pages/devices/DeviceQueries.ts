import { Device } from '@/types/types';
import { gql } from '@urql/core';

export const FRAGMENT_GET_DEVICE_STATUS = gql`
  fragment DeviceStatus on Device {
    status {
      lights {
        ison
      }
    }
  }
`;

/** Variables */
export type GetDeviceByIdVariable = {
  name: string;
};

/** Types */
export type GetDevicesQueryType = { __typename?: 'AppQueries' } & {
  devices: Array<Device>;
};

export const GET_DEVICE_BY_ID = gql`
  query GET_DEVICE_BY_ID($name: String!) {
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

export const GET_DEVICES = gql`
  query GetDevices {
    devices {
      id
      name
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
  ${FRAGMENT_GET_DEVICE_STATUS}
`;
