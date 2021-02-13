import { DeviceStatePayload } from '@/types/graphql/payloads';
import { gql } from '@urql/core';

/** Types */
export type SetLightStateQueryType = { __typename?: 'AppQueries' } & {
  setLightState: DeviceStatePayload;
};
/** Queries */
export const SET_LIGHT_STATE = gql`
  query SetLightState($input: DeviceLightStateInput!) {
    setLightState(input: $input) {
      errors {
        message
        code
      }
      message
      lightResponseType {
        ison
        red
        green
        blue
      }
    }
  }
`;
