import {NavigationGuardNext, RouteLocationNormalized} from 'vue-router';
import {Roles, Routes} from '@/types/enums';
import {useIdentity} from '@/hooks/useIdentity';

interface UseRouteAuthGuardParams {
    to: RouteLocationNormalized;
    from: RouteLocationNormalized;
    next: NavigationGuardNext;
}

export const useRouteAuthGuard = ({to, next}: UseRouteAuthGuardParams) => {
    if (to.matched.some((record) => record.meta.requiresAuth)) {
        validateIdentityAndGoToRoute(to, next);
    } else {
        const {autoLoginAlreadyDone} = useIdentity();
        if (to.matched.some((record) => record.meta.autoLogin) && !autoLoginAlreadyDone) {
            validateIdentityAndGoToRoute(to, next);
        } else {
            next();
        }
    }
};

const validateIdentityAndGoToRoute = (to: RouteLocationNormalized, next: NavigationGuardNext) => {
    const {isAuthenticated, refreshTokens} = useIdentity();
    if (isAuthenticated()) {
        validateUserRoleToRoute(to, next);
    } else {
        refreshTokens().then(() => {
            if (isAuthenticated()) {
                if (to.path === Routes.Login || to.path === Routes.Registration) {
                    next({path: Routes.Home});
                } else {
                    next({path: to.path});
                }
            } else {
                next({path: Routes.Login});
            }
        });
    }
};

const validateUserRoleToRoute = (to: RouteLocationNormalized, next: NavigationGuardNext) => {
    const {isRole} = useIdentity();
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
