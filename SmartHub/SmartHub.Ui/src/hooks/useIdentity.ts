import { AppActionTypes } from '@/store/app/actions';
import { Roles, Routes } from '@/types/enums';
import { useStorage } from '@vueuse/core';
import jwtDecode from 'jwt-decode';
import { Ref } from 'vue';
import router from '@/router';
import { store } from '@/store';

type TokenPayload = {
  unique_name: string;
  jti: string;
  roles: string[] | string;
  nbf: number;
  exp: number;
  iat: number;
};

type IdentityState = {
  token: Ref<string>;
  clearStorage: () => void;
  isAuthenticated: () => boolean;
  isRole: () => Roles;
  logout: () => void;
};

const numberThousand = 1000; // used for tokenpayload "exp date" conversion
const LOCAL_STORAGE_TOKEN = 'token';

export const useIdentity = (): IdentityState => {
  const token = useStorage(LOCAL_STORAGE_TOKEN, '');
  const isAuthenticated = () => token.value !== '';
  const clearStorage = () => {
    token.value = '';
  };

  const logout = async () => {
    clearStorage();
    await router.push(Routes.Login);
    await store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
    await store.dispatch(AppActionTypes.RESET_SIDEBAR);
  };

  const isRole = () => {
    if (token.value === '') {
      return Roles.None;
    }
    const tokenPayload = jwtDecode(token.value) as TokenPayload;
    if (Date.now() >= tokenPayload.exp * numberThousand) {
      return Roles.None;
    }
    const { roles } = tokenPayload;
    if (roles.includes('Admin')) {
      return Roles.Admin;
    }
    if (roles.includes('User')) {
      return Roles.User;
    }
    if (roles.includes('Guest')) {
      return Roles.Guest;
    }
    return Roles.None;
  };
  return {
    token,
    clearStorage,
    isAuthenticated,
    isRole,
    logout
  };
};
