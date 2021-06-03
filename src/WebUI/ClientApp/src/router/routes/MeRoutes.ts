import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';
import Me from '@/pages/me/Me.vue';

export const meRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Me,
    name: 'Me',
    component: Me,
    meta: {
      requiresAuth: true,
      isGuest: true
    }
  }
];
