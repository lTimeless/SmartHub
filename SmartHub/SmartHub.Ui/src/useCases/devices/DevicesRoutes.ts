import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const devicesRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Devices,
    name: 'Devices',
    component: () => import(/* webpackChunkName: "home" */ './Devices.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
