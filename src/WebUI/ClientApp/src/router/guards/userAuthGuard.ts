import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { Roles, Routes } from '@/types/enums';
import { useIdentity } from '@/hooks/useIdentity';

export const useRouteAuthGuard = async (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    validateIdentityAndGoToRoute(to, next);
  } else {
    if(to.matched.some((record) => record.meta.autoLogin)) {
      validateIdentityAndGoToRoute(to, next);
    }
    else {
      next({ path: Routes.Login });
    }
  }
};


const validateUserRoleToRoute = (to: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { isRole } = useIdentity();
  const role = isRole();
  if (to.matched.some((record) => record.meta.isAdmin)) {
    if (role === Roles.Admin) {
      next();
    } else {
      next({ path: Routes.NotAuthorized });
    }
  } else if (to.matched.some((record) => record.meta.isUser)) {
    if (role === Roles.Admin || role === Roles.User) {
      next();
    } else {
      next({ path: Routes.NotAuthorized });
    }
  } else if (to.matched.some((record) => record.meta.isGuest)) {
    if (role === Roles.Admin || role === Roles.User || role === Roles.Guest) {
      next();
    } else {
      next({ path: Routes.Login });
    }
  } else {
    next();
  }
};


const validateIdentityAndGoToRoute = async (to: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { isAuthenticated, refreshTokens } = useIdentity();
  if (isAuthenticated()) {
    validateUserRoleToRoute(to, next);
  }
  else {
    await refreshTokens();
    if (isAuthenticated()) {
      if (to.path === '/login') {
        next({path: Routes.Home});
      } else {
        validateUserRoleToRoute(to, next);
      }
    }
    else {
      next({ path: Routes.Login });
    }
  }
}