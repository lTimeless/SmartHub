import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const deviceRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Devices,
    name: 'Devices',
    component: () => import(/* webpackChunkName: "devices" */ '../../pages/devices/Devices.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  },
  {
    path: Routes.DeviceDetails,
    name: 'DeviceDetails',
    component: () => import(/* webpackChunkName: "devices" */ '../../pages/devices/DeviceDetails.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
