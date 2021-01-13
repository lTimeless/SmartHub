import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import { useRouteAuthGuard } from '@/router/guards/userAuthGuard';
import NotFound from '@/views/NotFound.vue';
import NotAuthorized from '@/views/NotAuthorized.vue';
import Statistics from '@/useCases/statistics/Statistics.vue';
import { Routes } from '@/types/enums';
import Layout from '@/views/Layout.vue';
import { identityRoutes } from '@/pages/identity/IdentityRoutes';
import { initRoutes } from '@/pages/init/InitRoutes';
import { homeRoutes } from '@/pages/home/HomeRoutes';
import { usersRoutes } from '@/pages/users/UsersRoutes';
import { meRoutes } from '@/pages/me/MeRoutes';
import { deviceRoutes } from '@/pages/devices/DeviceRoutes';
import { groupRoutes } from '@/pages/groups/GroupRoutes';
import { automationsRoutes } from '@/pages/automations/AutomationsRoutes';
import { adminRoutes } from '@/pages/admin/AdminRoutes';

const routes: Array<RouteRecordRaw> = [
  ...identityRoutes,
  ...initRoutes,
  {
    path: Routes.Layout,
    component: Layout,
    redirect: Routes.Home,
    meta: {
      requiresAuth: true,
      isGuest: true
    },
    children: [
      // Guest routes
      ...homeRoutes,
      ...meRoutes,
      ...groupRoutes,
      ...deviceRoutes,
      ...usersRoutes,
      ...automationsRoutes,
      // User routes
      {
        path: Routes.Configuration,
        name: 'Configuration',
        component: () =>
          import(/* webpackChunkName: "config" */ '../useCases/configurations/Configuration.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Plugins,
        name: 'Plugins',
        component: () => import(/* webpackChunkName: "plugins" */ '../useCases/plugins/Plugins.vue'),

        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Statistics,
        name: 'Statistics',
        component: Statistics,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      // Admin routes
      ...adminRoutes,
      // Error routes
      {
        path: Routes.NotAuthorized,
        name: 'NotAuthorized',
        component: NotAuthorized
      },
      {
        path: Routes.NotFound,
        name: 'NotFound',
        component: NotFound
      }
    ]
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  useRouteAuthGuard(to, from, next);
});

export default router;
