import { createApp } from 'vue';
import urql from '@urql/vue';
import IconPlugin from './plugins/IconPlugin';
import App from './App.vue';
import router from './router';
import { store } from './store';
import { client } from './graphql-client';

import '@/styles/main.css';
import ControlsPlugin from './plugins/ControlsPlugin';

const app = createApp(App)
  .use(urql, client)
  .use(router)
  .use(store)
  .use(IconPlugin)
  .use(ControlsPlugin);
// app.config.errorHandler((err, vueInstance, vueInfo) => {
//   // send to Sentry e.g
// });
app.config.performance = true;
app.mount('#app');
