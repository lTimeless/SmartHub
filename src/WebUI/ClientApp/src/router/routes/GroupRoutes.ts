import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const groupRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Groups,
    name: 'Groups',
    component: () => import(/* webpackChunkName: "groups" */ '../../pages/groups/Groups.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  },
  {
    path: Routes.GroupDetails,
    name: 'GroupDetail',
    component: () => import(/* webpackChunkName: "groups" */ '../../pages/groups/GroupDetails.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
