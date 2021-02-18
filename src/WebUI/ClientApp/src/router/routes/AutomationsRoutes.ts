import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const automationsRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Automations,
    name: 'Automations',
    component: () => import(/* webpackChunkName: "init" */ '../../pages/automations/Automations.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
