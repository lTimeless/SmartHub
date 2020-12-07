/* eslint-disable prettier/prettier */
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';
import './assets/tailwind.css';
import { DefaultApolloClient } from '@vue/apollo-composable';
import { apolloClient } from './vue-apollo';

const app = createApp(App)
  .provide(DefaultApolloClient, apolloClient)
  .use(router)
  .use(store);

// app.config.errorHandler((err, vueInstance, vueInfo) => {
//   // send to Sentry e.g
// });
app.config.performance = true;
app.mount('#app');
