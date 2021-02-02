import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const initRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Init,
    name: 'init',
    component: () => import(/* webpackChunkName: "init" */ './Init.vue'),

    meta: {
      requiresAuth: false
    }
  }
];
