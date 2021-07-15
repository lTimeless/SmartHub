import DevicesPage from 'src/pages/devices';
import HomePage from 'src/pages/home';
import { Routes } from 'src/types/enums';
import LoginPage from 'src/pages/identity/login';
import RegistrationPage from 'src/pages/identity/registration';
import NotFoundPage from 'src/pages/errors/NotFoundPage';
import NotAuthorizedPage from 'src/pages/errors/NotAuthorizedPage';
import IRoute from '../types/route';

export const publicRoutes: IRoute[] = [
  {
    path: Routes.Login,
    exact: true,
    auth: false,
    component: LoginPage,
    name: 'Login'
  },
  {
    path: Routes.Register,
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
  }
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
  // Error
  {
    path: Routes.NotFound,
    exact: false,
    auth: true,
    component: NotFoundPage,
    name: 'NotFound'
  },
  {
    path: Routes.NotAuthorized,
    exact: false,
    auth: true,
    component: NotAuthorizedPage,
    name: 'NotAuthorized'
  }
];
