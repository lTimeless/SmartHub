import {DevicesPage} from "@/pages/devices/devices";
import HomePage from "@/pages/home/home";
import {Routes} from "@/types/enums";
import LoginPage from "src/pages/identity/login/login";
import RegistrationPage from "src/pages/identity/registration/registration";
import Layout from "src/pages/layout/layout";
import IRoute from "../types/route";

const authRoutes: IRoute[] = [
    {
        path: Routes.Login,
        exact: true,
        auth: false,
        component: LoginPage,
        name: 'Login'
    },
    {
        path: Routes.Registration,
        exact: true,
        auth: false,
        component: RegistrationPage,
        name: 'Registration'
    }
];

const baseRoutes: IRoute[] = [
    {
        path: Routes.Init,
        exact: true,
        auth: false,
        component: RegistrationPage,
        name: 'Init'
    },
    {
        path: Routes.Layout,
        exact: true,
        auth: true,
        component: Layout,
        name: 'Layout'
    },
];

export const inAppRoutes: IRoute[] = [
    {
        path: Routes.Home,
        exact: true,
        auth: true,
        component: HomePage,
        name: 'Home'
    },
    {
        path: Routes.Devices,
        exact: true,
        auth: true,
        component: DevicesPage,
        name: 'Devices'
    },
];


export const routes: IRoute[] = [
    ...baseRoutes,
    ...authRoutes
];
