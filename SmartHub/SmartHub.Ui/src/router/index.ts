import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";
import Login from '@/views/auth/Login.vue';
import Registration from '@/views/auth/Registration.vue';
import Notfound from "@/views/NotFound.vue";
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


Vue.use(VueRouter);

const routes: Array<RouteConfig> = [

  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/registration",
    name: "Registration",
    component: Registration
  },
  {
    path: "",
    component: Home,
    beforeEnter: (to, from, next) => {
      console.log("beforeEnter Home", to , from);
      //TODO: is Authenticated logic implementiren
      next();
    },
    children: [
      {
        path: "/",
        name: "Home",
        component: Dashboard
      },
      {
        path: "/settings",
        name: "Settings",
        component: Settings
      },
      {
        path: "/about",
        name: "About",
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () =>
            import(/* webpackChunkName: "about" */ "../views/home/About.vue")
      },
      {
        path: "/plugins",
        name: "Plugins",
        component: Plugins
      },
      {
        path: "/routines",
        name: "Routines",
        component: Routines
      },
      {
        path: "/statistics",
        name: "Statistics",
        component: Statistics
      },
      {
        path: "/activity",
        name: "Activity",
        component: Activity
      },
      {
        path: "/logs",
        name: "Logs",
        component: Logs
      },
      {
        path: "/system",
        name: "System",
        component: System
      },
      {
        path: "/health",
        name: "Health",
        component: Health
      },
      {
        path: "/manager",
        name: "Manager",
        component: Manager
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

export default router;
