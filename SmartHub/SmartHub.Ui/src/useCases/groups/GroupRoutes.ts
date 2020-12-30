import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const groupRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Groups,
    name: 'Groups',
    component: () => import(/* webpackChunkName: "groups" */ './Groups.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
