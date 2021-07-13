import DevicesPage from "@/pages/devices";
import HomePage from "@/pages/home";
import {Routes} from "@/types/enums";
import LoginPage from "@/pages/identity/login";
import RegistrationPage from "@/pages/identity/registration";
import IRoute from "../types/route";

export const publicRoutes: IRoute[] = [
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
    },
    {
        path: Routes.Init,
        exact: true,
        auth: false,
        component: RegistrationPage,
        name: 'Init'
    },
];

export const protectedRoutes: IRoute[] = [
    {
        path: Routes.Home,
        exact: false,
        auth: true,
        component: HomePage,
        name: 'Home'
    },
    {
        path: Routes.Devices,
        exact: false,
        auth: true,
        component: DevicesPage,
        name: 'Devices'
    },
];

