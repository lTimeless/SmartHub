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
import { Routes } from '@/types/enums';
import Home from '@/views/Home.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/registration',
    name: 'Registration',
    component: Registration,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/init',
    name: 'init',
    component: Init,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/',
    component: Home,
    meta: {
      requiresAuth: true,
      isGuest: true
    },
    children: [
      // Guest paths #####
      {
        path: '',
        name: 'Dashboard',
        component: () => import(/* webpackChunkName: "dashboard" */ '../views/home/Dashboard.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: '/me',
        name: 'Me',
        component: () => import(/* webpackChunkName: "user" */ '../views/home/Me.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: '/groups',
        name: 'Groups',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Groups.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Devices,
        name: 'Devices',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Devices.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Users,
        name: 'Users',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Users.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: Routes.Automations,
        name: 'Automations',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Automations.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      // {
      //   path: Routes.About,
      //   name: 'About',
      //   // route level code-splitting
      //   // this generates a separate chunk (about.[hash].js) for this route
      //   // which is lazy-loaded when the route is visited.
      //   component: () => import(/* webpackChunkName: "about" */ '../views/home/About.vue'),
      //   meta: {
      //     requiresAuth: true,
      //     isGuest: true
      //   }
      // },
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
