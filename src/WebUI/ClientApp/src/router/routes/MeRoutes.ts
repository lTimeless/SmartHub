import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const meRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Me,
    name: 'Me',
    component: () => import(/* webpackChunkName: "me" */ '../../pages/me/Me.vue'),
    meta: {
      requiresAuth: true,
      isGuest: true
    }
  }
];
