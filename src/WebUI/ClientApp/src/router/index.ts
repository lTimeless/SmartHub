import {createRouter, createWebHistory, RouteRecordRaw} from 'vue-router';
import {useRouteAuthGuard} from '@/router/guards/userAuthGuard';
import NotAuthorized from '@/pages/errors/NotAuthorized.vue';
import Statistics from '@/pages/statistics/Statistics.vue';
import {Routes} from '@/types/enums';
import Layout from '@/pages/layout/Layout.vue';
import {identityRoutes} from '@/router/routes/IdentityRoutes';
import {initRoutes} from '@/router/routes/InitRoutes';
import {homeRoutes} from '@/router/routes/HomeRoutes';
import {usersRoutes} from '@/router/routes/UsersRoutes';
import {automationsRoutes} from '@/router/routes/AutomationsRoutes';
import {adminRoutes} from '@/router/routes/AdminRoutes';
import NotFound from '@/pages/errors/NotFound.vue';
import {deviceRoutes} from './routes/DeviceRoutes';
import {groupRoutes} from './routes/GroupRoutes';
import {meRoutes} from './routes/MeRoutes';

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
        component: () => import(/* webpackChunkName: "config" */ '../pages/configuration/Configuration.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Plugins,
        name: 'Plugins',
        component: () => import(/* webpackChunkName: "plugins" */ '../pages/plugins/Plugins.vue'),
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
  useRouteAuthGuard({to: to, from: from, next: next});
});

export default router;
