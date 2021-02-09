import { Payload } from '@/types/graphql/payloads';
import { gql, useQuery, UseQueryResponse } from '@urql/vue';

export type DeviceLightStateInput = {
  deviceId: string;
  setLight: boolean;
};

export type LightResponseType = {
  __typename?: 'LightResponseType';
  ison: boolean;
  mode?: string;
  red: number;
  green: number;
  blue: number;
  white: number;
};

export type SetLightStateQueryVariables = {
  input: DeviceLightStateInput;
};

export interface DeviceStatePayload extends Payload {
  __typename?: 'DeviceStatePayload';
  lightResponseType?: LightResponseType;
}

export type SetLightStateQueryType = { __typename?: 'AppQueries' } & {
  setLightState: DeviceStatePayload;
};

export const SetLightStateDocument = gql`
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

export const useSetLightStateQuery = ( variables: SetLightStateQueryVariables): UseQueryResponse<SetLightStateQueryType, unknown> =>
  useQuery<SetLightStateQueryType>({ query: SetLightStateDocument, variables: variables });
