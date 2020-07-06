import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";
import Login from '@/views/auth/Login.vue';
import Registration from '@/views/auth/Registration.vue';
import Notfound from "@/views/NotFound.vue";
import Dashboard from "@/views/home/Dashboard.vue";
import Settings from "@/views/home/Settings.vue";


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
            import(/* webpackChunkName: "about" */ "../views/About.vue")
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
