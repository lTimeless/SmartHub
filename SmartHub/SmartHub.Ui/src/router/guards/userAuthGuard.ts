import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { getToken, getUserRole, isAuthenticated } from '@/services/auth/authService';
import { Roles } from '@/types/enums';

const validateUserRoleToRoute = (to: RouteLocationNormalized, roles: Roles, next: NavigationGuardNext) => {
  if (to.matched.some((record) => record.meta.isAdmin)) {
    if (roles === Roles.Admin) {
      next();
    } else {
      next({ name: 'NotAuthorized' });
    }
  } else if (to.matched.some((record) => record.meta.isUser)) {
    if (roles === Roles.Admin || roles === Roles.User) {
      next();
    } else {
      next({ name: 'NotAuthorized' });
    }
  } else if (to.matched.some((record) => record.meta.isGuest)) {
    if (roles === Roles.Admin || roles === Roles.User || roles === Roles.Guest) {
      next();
    } else {
      next({ name: 'NotAuthorized' });
    }
  }
};

export const useRouteAuthGuard = (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!isAuthenticated()) {
      next({ name: 'Login' });
    } else {
      //  anstatt den authresponse zu nehmen um die rollen zu prüfen
      // TODO: vlt den token nehmen ans BE schicken- prüfen lassen ob es noch valide ist und darauf dann userberechtigungen/authresponse bekommen
      const token = getToken();
      if (token === null) {
        next({ name: 'Login' });
      } else {
        validateUserRoleToRoute(to, getUserRole(), next);
      }
    }
  } else {
    next();
  }
};
