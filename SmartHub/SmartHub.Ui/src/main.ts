import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';
import './assets/tailwind.css';
import vueTour from 'vue-tour/src/main';

const app = createApp(App)
  .use(vueTour)
  .use(router)
  .use(store);

  // app.config.errorHandler((err, vueInstance, vueInfo) => {
  //   // send to Sentry e.g
  // });
app.config.performance = true;
app.mount('#app');
