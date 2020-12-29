import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const homeRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Home,
    name: 'Home',
    component: () => import(/* webpackChunkName: "dashboard" */ './Home.vue'),
    meta: {
      requiresAuth: true,
      isGuest: true
    }
  }
];
