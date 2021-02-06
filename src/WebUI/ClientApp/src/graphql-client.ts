import { authExchange } from '@urql/exchange-auth';
import { createClient, dedupExchange, cacheExchange, fetchExchange } from '@urql/vue';

export const client = createClient({
  url: '/graphql',
  fetchOptions: () => {
    const token = localStorage.getItem('token');
    return {
      headers: { authorization: token ? `Bearer ${token}` : '' }
    }
  },
  exchanges: [
    dedupExchange,
    cacheExchange,
    // authExchange({

    // }),
    fetchExchange
  ]
});