import { Roles } from '@/types/enums';
import { useStorage } from '@vueuse/core';
import jwtDecode from 'jwt-decode';
import { Ref } from 'vue';

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
};

const numberThousand = 1000; // used for tokenpayload "exp date" conversion
const LOCAL_STORAGE_TOKEN = 'token';

export const useIdentity = (): IdentityState => {
  const token = useStorage(LOCAL_STORAGE_TOKEN, '');
  const isAuthenticated = () => token.value !== '';
  const clearStorage = () => {
    token.value = '';
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
    isRole
  };
}
