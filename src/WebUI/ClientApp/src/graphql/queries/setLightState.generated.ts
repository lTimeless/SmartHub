import * as Types from '../graphql.types';

import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type SetLightStateQueryVariables = Types.Exact<{
  input: Types.DeviceLightStateInput;
}>;

export type SetLightStateQuery = {
  __typename?: 'AppQueries';
  setLightState: {
    __typename?: 'DeviceStatePayload';
    message?: Types.Maybe<string>;
    errors?: Types.Maybe<Array<{ __typename?: 'UserError'; message: string; code: Types.AppErrorCodes }>>;
    lightResponseType?: Types.Maybe<{
      __typename?: 'LightResponseType';
      ison: boolean;
      red: number;
      green: number;
      blue: number;
    }>;
  };
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

export function useSetLightStateQuery(
  options: Omit<Urql.UseQueryArgs<SetLightStateQueryVariables>, 'query'> = {}
) {
  return Urql.useQuery<SetLightStateQuery>({ query: SetLightStateDocument, ...options });
}
