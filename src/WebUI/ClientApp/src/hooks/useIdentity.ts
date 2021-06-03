import { AppActionTypes } from '@/store/app/actions';
import { Roles, Routes } from '@/types/enums';
import router from '@/router';
import { store } from '@/store';
import { ref } from 'vue';
import { useCookies } from '@vueuse/integrations';

// TODO rewrite without localstorage usage, token are now httpOnly cookies
//  save roles, isAuthenticated and userId from response in here
const userRoles = ref<string[] | undefined>(undefined);
const authenticated = ref<boolean>(false);
const userId = ref<string | undefined | null>(undefined);

export const useIdentity = () => {
  const isAuthenticated = () => authenticated.value !== false;
  const accessTokenAvailable = () => {
    const cookies = useCookies(['SmartHub-Access-Token', 'SmartHub-Refresh-Token'], { doNotParse: false, autoUpdateDependencies: false });
    const all = cookies.getAll();
    console.log(all,cookies.get('SmartHub-Access-Token'));
    return cookies;
  };

  const setIdentity = (roles: string[], id: string, isAuthenticated: boolean) => {
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

  return {
    roles: userRoles,
    authenticated,
    userId,
    accessTokenAvailable,
    setIdentity,
    clearIdentity,
    isAuthenticated,
    isRole,
    logout
  };
};
