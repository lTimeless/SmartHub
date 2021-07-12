import {RouteRecordRaw} from 'vue-router';
import {Routes} from '@/types/enums';
import Login from '@/pages/indentity/login/Login.vue';
import Registration from '@/pages/indentity/registration/Registration.vue';

export const identityRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Login,
    name: 'Login',
    component: Login,
    meta: {
      requiresAuth: false,
      autoLogin: true
    }
  },
  {
    path: Routes.Registration,
    name: 'Registration',
    component: Registration,
    meta: {
        requiresAuth: false,
        autoLogin: true
    }
  }
];
