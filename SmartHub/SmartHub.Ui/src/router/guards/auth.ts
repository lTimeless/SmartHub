import { NavigationGuardNext, Route } from 'vue-router';
import { AuthResponse } from '@/types/types';

const validateUserRoleToRoute = (to: Route, roles: string[], next: NavigationGuardNext) => {
  if (to.matched.some((record) => record.meta.isAdmin)) {
    if (roles.includes('Admin')) {
      next();
    } else {
      next({ path: '/notauth' });
    }
  } else if (to.matched.some((record) => record.meta.isUser)) {
    if (roles.some((role) => role === 'Admin' || role === 'User')) {
      next();
    } else {
      next({ path: '/notauth' });
    }
  } else if (to.matched.some((record) => record.meta.isGuest)) {
    if (roles.some((role) => role === 'Admin' || role === 'User' || role === 'Guest')) {
      next();
    } else {
      next({ path: '/notauth' });
    }
  }
};

export const routeAuthGuard = (to: Route, from: Route, next: NavigationGuardNext) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    const loginResponseLocalStorage = localStorage.getItem('loginResponse');
    if (loginResponseLocalStorage === null) {
      next({ name: 'Login' });
    } else {
      const loginResponse = JSON.parse(loginResponseLocalStorage) as AuthResponse;
      if (loginResponse === null) {
        next({ name: 'Login' });
      } else {
        validateUserRoleToRoute(to, loginResponse.roles, next);
      }
    }
  } else {
    next();
  }
};
