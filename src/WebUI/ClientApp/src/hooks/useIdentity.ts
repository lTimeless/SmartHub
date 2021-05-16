import { AppActionTypes } from '@/store/app/actions';
import { Roles, Routes } from '@/types/enums';
import router from '@/router';
import { store } from '@/store';
import { ref } from 'vue';

// TODO rewrite without localstorage usage, token are now httpOnly cookies
//  save roles, isAuthenticated and userId from response in here

export const useIdentity = () => {
  const roles = ref<string[] | undefined>(undefined);
  const authenticated = ref<boolean>(false);
  const userId = ref<string | undefined>(undefined);

  const isAuthenticated = () => authenticated.value !== false;

  const clearIdentity = () => {
    roles.value = undefined;
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
    if (!roles.value) {
      return Roles.None;
    } else if (roles.value.includes('Admin')) {
      return Roles.Admin;
    } else if (roles.value.includes('User')) {
      return Roles.User;
    } else if (roles.value.includes('Guest')) {
      return Roles.Guest;
    } else {
      return Roles.None;
    }
  };

  return {
    roles,
    authenticated,
    userId,
    clearIdentity,
    isAuthenticated,
    isRole,
    logout
  };
};
