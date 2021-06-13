import { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
import { Roles, Routes } from '@/types/enums';
import { useIdentity } from '@/hooks/useIdentity';

const validateUserRoleToRoute = (to: RouteLocationNormalized, next: NavigationGuardNext) => {
  const { clearIdentity, isRole } = useIdentity();
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
      clearIdentity();
      next({ path: Routes.Login });
    }
  }
};

export const useRouteAuthGuard = (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
): void => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    const { isAuthenticated } = useIdentity();
    // TODO: BE call machen wenn Token noch im storage ist, wenn der noch gültig ist dann weiter zum dashboard wenn nicht dann einen neuen beantragen
    // Refreshtoken!!!!
    if (!isAuthenticated()) {
      // TODO try to refresh Tokens = call BE endpoint
      // if server response is still not Authenticated than relogin (and create toast with error message "Not authorized")
      // if than true than call "validateUserRoleToRoute()"
      next({ path: Routes.Login });
    } else {
      validateUserRoleToRoute(to, next);
    }
  } else {
    next();
  }
};
