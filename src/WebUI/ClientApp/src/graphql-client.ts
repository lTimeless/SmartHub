import { authExchange } from '@urql/exchange-auth';
import { createClient, dedupExchange, fetchExchange, cacheExchange } from '@urql/vue';
import { devtoolsExchange } from '@urql/devtools';
// import { cacheExchange } from '@urql/exchange-graphcache';


export const client = createClient({
  url: '/graphql',
  fetchOptions: () => {
    const token = localStorage.getItem('token');
    return {
      headers: { authorization: token ? `Bearer ${token}` : '' }
    };
  },
  exchanges: [
    devtoolsExchange,
    dedupExchange,
    cacheExchange,
    // cacheExchange({
    //   updates: {
    //     Mutation: {
    //       updateUser: (_result, _args, cache) => {
    //         cache.updateQuery({ query: WHO_AM_I }, (data) => data);
    //       },
    //       createDevice: (result: any, _args, cache) => {
    //         cache.updateQuery({ query: GET_DEVICES }, (data: any) => {
    //           console.log(data, result);
    //           return {
    //             ...data,
    //             devices: [...data?.devices, result?.createDevice.device]
    //           };
    //         });
    //       },
    //       createGroup: (result: any, _args, cache) => {
    //         cache.updateQuery({ query: GET_GROUPS }, (data: any) => {
    //           console.log(data, result);
    //           return {
    //             ...data,
    //             devices: [...data?.groups, result?.createGroup.group]
    //           };
    //         });
    //       }
    //     }
    //   }
    // }),
    // authExchange({

    // }),
    fetchExchange
  ]
});
