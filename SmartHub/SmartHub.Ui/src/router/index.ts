import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import { useRouteAuthGuard } from '@/router/guards/userAuthGuard';
import NotFound from '@/views/NotFound.vue';
import NotAuthorized from '@/views/NotAuthorized.vue';
import Login from '@/views/auth/Login.vue';
import Registration from '@/views/auth/Registration.vue';
import Init from '@/views/Init.vue';
import Statistics from '@/views/home/Statistics.vue';
import Events from '@/views/home/admin/Events.vue';
import Logs from '@/views/home/admin/Logs.vue';
import System from '@/views/home/admin/System.vue';
import Health from '@/views/home/admin/Health.vue';
import Manager from '@/views/home/admin/Manager.vue';

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
    component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue'),
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
        path: '/user',
        name: 'User',
        component: () => import(/* webpackChunkName: "user" */ '../views/home/MyUser.vue'),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: '/about',
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
        path: '/settings',
        name: 'Settings',
        component: () => import(/* webpackChunkName: "init" */ '../views/home/Settings.vue'),
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: '/plugins',
        name: 'Plugins',
        component: () => import(/* webpackChunkName: "plugins" */ '../components/AppAutomations.vue'),

        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: '/statistics',
        name: 'Statistics',
        component: Statistics,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      // Admin paths #####
      {
        path: '/events',
        name: 'events',
        component: Events,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: '/logs',
        name: 'Logs',
        component: Logs,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: '/system',
        name: 'System',
        component: System,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: '/health',
        name: 'Health',
        component: Health,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: '/manager',
        name: 'Manager',
        component: Manager,
        meta: {
          requiresAuth: true,
          isAdmin: true
        }
      },
      {
        path: '/notauth',
        name: 'NotAuthorized',
        component: NotAuthorized
      },
      {
        path: '/:catchAll(.*)',
        component: NotFound
      }
    ]
  },
  {
    path: '/:catchAll(.*)',
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
