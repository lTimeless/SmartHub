import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const homeRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Home,
    name: 'Home',
    component: () => import(/* webpackChunkName: "home" */ '../../pages/home/Home.vue'),
    meta: {
      requiresAuth: true,
      isGuest: true
    }
  }
];
