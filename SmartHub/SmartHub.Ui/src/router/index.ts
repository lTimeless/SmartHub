import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";
import Login from '@/views/auth/Login.vue';
import Registration from '@/views/auth/Registration.vue';
import Notfound from "@/views/NotFound.vue";
import NotAuthorized from "@/views/NotAuthorized.vue";
import Dashboard from "@/views/home/Dashboard.vue";
import Settings from "@/views/home/Settings.vue";
import Plugins from "@/views/home/Plugins.vue";
import Routines from "@/views/home/Routines.vue";
import Statistics from "@/views/home/Statistics.vue";

import Activity from "@/views/home/admin/Activity.vue";
import Logs from "@/views/home/admin/Logs.vue";
import System from "@/views/home/admin/System.vue";
import Health from "@/views/home/admin/Health.vue";
import Manager from "@/views/home/admin/Manager.vue";
import { routeAuthGuard } from "@/router/guards/auth";


Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/login",
    name: "Login",
    component: Login,
    meta: {
      requiresAuth: false,
    }
  },
  {
    path: "/registration",
    name: "Registration",
    component: Registration,
    meta: {
      requiresAuth: false,
    }
  },
  {
    path: "",
    component: Home,
    meta: {
      requiresAuth: true,
      isGuest: true
    },
    children: [
      {
        path: "/",
        name: "Home",
        component: Dashboard,
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: "/settings",
        name: "Settings",
        component: Settings,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: "/about",
        name: "About",
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () =>
            import(/* webpackChunkName: "about" */ "../views/home/About.vue"),
        meta: {
          requiresAuth: true,
          isGuest: true
        }
      },
      {
        path: "/plugins",
        name: "Plugins",
        component: Plugins,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: "/routines",
        name: "Routines",
        component: Routines,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: "/statistics",
        name: "Statistics",
        component: Statistics,
        meta: {
          requiresAuth: true,
          isUser: true
        }
      },
      {
        path: "/activity",
        name: "Activity",
        component: Activity,
        meta: {
          requiresAuth: true,
          isAdmin : true
        }
      },
      {
        path: "/logs",
        name: "Logs",
        component: Logs,
        meta: {
          requiresAuth: true,
          isAdmin : true
        }
      },
      {
        path: "/system",
        name: "System",
        component: System,
        meta: {
          requiresAuth: true,
          isAdmin : true
        }
      },
      {
        path: "/health",
        name: "Health",
        component: Health,
        meta: {
          requiresAuth: true,
          isAdmin : true
        }
      },
      {
        path: "/manager",
        name: "Manager",
        component: Manager,
        meta: {
          requiresAuth: true,
          isAdmin : true
        }
      },
      {
        path: "/notauth",
        name: "NotAuthorized",
        component: NotAuthorized
      },
      {
        path: "*",
        component: Notfound
      }
    ]
  },
  {
    path: "*",
    component: Notfound
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  routeAuthGuard(to , from, next);
})


export default router;
