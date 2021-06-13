import { AppActionTypes } from '@/store/app/actions';
import { Roles, Routes } from '@/types/enums';
import router from '@/router';
import { store } from '@/store';
import { ref } from 'vue';
import { Maybe } from '@/graphql/graphql.types';

const userId = ref<Maybe<string>>(undefined);
const userRoles = ref<Maybe<string[]>>(undefined);
const authenticated = ref<boolean>(false);

export const useIdentity = () => {
  const isAuthenticated = () => authenticated.value !== false;
  const setIdentity = (roles: Maybe<string[]>, id: Maybe<string>, isAuthenticated: Maybe<boolean>) => {
    userRoles.value = roles ?? userRoles.value;
    userId.value = id ?? userId.value;
    authenticated.value = isAuthenticated ?? authenticated.value;
    console.log(isAuthenticated, authenticated.value);
  };

  const clearIdentity = () => {
    userRoles.value = undefined;
    authenticated.value = false;
    userId.value = undefined;
  };

  const logout = async () => {
    clearIdentity();
    await router.push(Routes.Login);
    await store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
    await store.dispatch(AppActionTypes.RESET_SIDEBAR);
  };

  const isRole = () => {
    if (!userRoles.value) {
      return Roles.None;
    } else if (userRoles.value.includes('Admin')) {
      return Roles.Admin;
    } else if (userRoles.value.includes('User')) {
      return Roles.User;
    } else if (userRoles.value.includes('Guest')) {
      return Roles.Guest;
    } else {
      return Roles.None;
    }
  };

  const refreshTokens = () => {
    // TODO call BE endpoint via gql
    return;
  };

  return {
    roles: userRoles,
    authenticated,
    userId,
    setIdentity,
    refreshTokens,
    clearIdentity,
    isAuthenticated,
    isRole,
    logout
  };
};
