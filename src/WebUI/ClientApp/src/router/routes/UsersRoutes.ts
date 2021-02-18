import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';

export const usersRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Users,
    name: 'Users',
    component: () => import(/* webpackChunkName: "home" */ '../../pages/users/Users.vue'),
    meta: {
      requiresAuth: true,
      isUser: true
    }
  }
];
