import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const groupsRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Groups,
    name: 'Groups',
    component: () => import(/* webpackChunkName: "home" */ './Groups.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
