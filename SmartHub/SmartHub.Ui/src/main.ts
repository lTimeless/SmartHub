/* eslint-disable prettier/prettier */
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';
import '@/assets/styles/tailwind.css';
import '@/assets/styles/main.css';
import { DefaultApolloClient } from '@vue/apollo-composable';
import { apolloClient } from './apollo';
import IconPlugin from '@/plugins/IconPlugin';

const app = createApp(App)
  .provide(DefaultApolloClient, apolloClient)
  .use(router)
  .use(store)
  .use(IconPlugin);
// app.config.errorHandler((err, vueInstance, vueInfo) => {
//   // send to Sentry e.g
// });
app.config.performance = true;
app.mount('#app');
