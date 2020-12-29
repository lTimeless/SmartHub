import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const identityRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Login,
    name: 'Login',
    component: () => import(/* webpackChunkName: "identity" */ './login/Login.vue'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: Routes.Registration,
    name: 'Registration',
    component: () => import(/* webpackChunkName: "identity" */ './registration/Registration.vue'),
    meta: {
      requiresAuth: false
    }
  }
]