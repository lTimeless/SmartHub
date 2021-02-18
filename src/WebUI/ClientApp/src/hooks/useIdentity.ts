import { AppActionTypes } from '@/store/app/actions';
import { Roles, Routes } from '@/types/enums';
import { useStorage } from '@vueuse/core';
import { Ref } from 'vue';
import router from '@/router';
import { store } from '@/store';
import { useJwt } from '@vueuse/integrations'

type TokenPayload = {
  unique_name: string;
  jti: string;
  roles: string[] | string;
  nbf: number;
  exp: number;
  iat: number;
};

const numberThousand = 1000; // used for tokenpayload "exp date" conversion
const LOCAL_STORAGE_TOKEN = 'token';

export const useIdentity = () => {
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
    const { payload: tokenPayload } = useJwt<TokenPayload>(token.value);
    if (tokenPayload.value && Date.now() >= tokenPayload.value.exp * numberThousand) {
      return Roles.None;
    }
    const roles = tokenPayload.value?.roles;
    if(!roles) {
      return Roles.None;
    }
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
