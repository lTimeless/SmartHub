import { createClient, dedupExchange, fetchExchange, cacheExchange } from '@urql/vue';
// import { devtoolsExchange } from '@urql/devtools';

export const client = createClient({
  url: '/graphql',
  // fetchOptions: () => {
  //   const token = localStorage.getItem('token');
  //   return {
  //     headers: { authorization: token ? `Bearer ${token}` : '' }
  //   };
  // },
  exchanges: [dedupExchange, cacheExchange, fetchExchange]
});
