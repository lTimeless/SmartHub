import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';
import './assets/tailwind.css';

const app = createApp(App).use(router).use(store);

// app.config.errorHandler((err, vueInstance, vueInfo) => {
//   // send to Sentry e.g
// });
app.config.performance = true;
app.mount('#app');
