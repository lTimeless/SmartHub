import * as Types from '../graphql.types';

import gql from 'graphql-tag';
export type DeviceStatusFragment = {
  __typename?: 'Device';
  status?: Types.Maybe<{
    __typename?: 'StatusResponseType';
    lights: Array<{ __typename?: 'LightResponseType'; ison: boolean }>;
  }>;
};

export const DeviceStatusFragmentDoc = gql`
  fragment DeviceStatus on Device {
    status {
      lights {
        ison
      }
    }
  }
`;
