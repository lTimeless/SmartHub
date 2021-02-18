import { RouteRecordRaw } from 'vue-router';
import { Routes } from '@/types/enums';
import Activity from '../../pages/admin/Activity.vue';
import Logs from '../../pages/admin/Logs.vue';
import System from '../../pages/admin/System.vue';
import Health from '../../pages/admin/Health.vue';

export const adminRoutes: Array<RouteRecordRaw> = [
  {
    path: Routes.Activity,
    name: 'activity',
    component: Activity,
    meta: {
      requiresAuth: true,
      isAdmin: true
    }
  },
  {
    path: Routes.Logs,
    name: 'Logs',
    component: Logs,
    meta: {
      requiresAuth: true,
      isAdmin: true
    }
  },
  {
    path: Routes.System,
    name: 'System',
    component: System,
    meta: {
      requiresAuth: true,
      isAdmin: true
    }
  },
  {
    path: Routes.Health,
    name: 'Health',
    component: Health,
    meta: {
      requiresAuth: true,
      isAdmin: true
    }
  }
];
