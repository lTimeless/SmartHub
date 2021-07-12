import * as Types from '../graphql.types';

import { IdUserNameFragment } from '../fragments/userFragment.generated';
import gql from 'graphql-tag';
import { IdUserNameFragmentDoc } from '../fragments/userFragment.generated';
import * as Urql from '@urql/vue';
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type GetMeQueryVariables = Types.Exact<{ [key: string]: never }>;

export type GetMeQuery = {
  __typename?: 'AppQueries';
  getMe: {
    __typename?: 'IdentityPayload';
    user?: Types.Maybe<
      {
        __typename?: 'User';
        createdAt: any;
        createdBy: string;
        lastModifiedAt: any;
        lastModifiedBy: string;
        phoneNumber?: Types.Maybe<string>;
        personInfo: string;
        email?: Types.Maybe<string>;
        personName: { __typename?: 'PersonName'; firstName: string; lastName: string; middleName: string };
      } & IdUserNameFragment
    >;
  };
};

export const GetMeDocument = gql`
  query GetMe {
    getMe {
      user {
        ...IdUserName
        createdAt
        createdBy
        lastModifiedAt
        lastModifiedBy
        phoneNumber
        personInfo
        email
        personName {
          firstName
          lastName
          middleName
        }
      }
    }
  }
  ${IdUserNameFragmentDoc}
`;

export function useGetMeQuery(options: Omit<Urql.UseQueryArgs<GetMeQueryVariables>, 'query'> = {}) {
  return Urql.useQuery<GetMeQuery>({ query: GetMeDocument, ...options });
}
