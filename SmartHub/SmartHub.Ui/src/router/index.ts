import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import { useRouteAuthGuard } from '@/router/guards/userAuthGuard';
import NotFound from '@/views/NotFound.vue';
import NotAuthorized from '@/views/NotAuthorized.vue';
import Login from '@/views/auth/Login.vue';
import Registration from '@/views/auth/Registration.vue';
import Init from '@/views/Init.vue';
import Statistics from '@/views/home/Statistics.vue';
import Activity from '@/views/home/admin/Activity.vue';
import Logs from '@/views/home/admin/Logs.vue';
import System from '@/views/home/admin/System.vue';
import Health from '@/views/home/admin/Health.vue';
import Manager from '@/views/home/admin/Manager.vue';
import { Routes } from '@/types/enums';

const routes: Array<RouteRecordRaw> = [
  {
    path: Routes.Login,
    name: 'Login',
    component: Login,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: Routes.Registration,
    name: 'Registration',
    component: Registration,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: Routes.Init,
    name: 'init',
    component: Init,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: Routes.Home,
    component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue'),
    meta: {
      requiresAuth: true,
      isGuest: true
    },
    children: [
      // Guest paths #####
      {
        path: Routes.Dashboard,
        name: 'Dashboard',
        component: () => import(/* webpackChunkName: "dashboard" */ '../views/home/Dashboard.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: Routes.Me,
        name: 'Me',
        component: () => import(/* webpackChunkName: "user" */ '../views/home/Me.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: Routes.About,
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/home/About.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      // User paths #####
      {
        path: Routes.Configuration,
        name: 'Configuration',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Configuration.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Plugins,
        name: 'Plugins',
        component: () => import(/* webpackChunkName: "plugins" */ '../views/home/Plugins.vue'),

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
      // Admin paths #####
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
      },
      {
        path: Routes.Manager,
        name: 'Manager',
        component: Manager,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: Routes.NotAuthorized,
        name: 'NotAuthorized',
        component: NotAuthorized
      },
      {
        path: Routes.NotFound,
        component: NotFound
      }
    ]
  },
  {
    path: Routes.NotFound,
    component: NotFound
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
