import { AuthResponse } from '@/types/types';
import store from '@/store/index';

type AuthType = { isAdmin: boolean; isUser: boolean; isGuest: boolean };

export const getUserRole = (): AuthType => {
  const authResponse = store.getters.getAuthResponse as AuthResponse;
  if (authResponse == null) {
    return { isAdmin: false, isUser: false, isGuest: false };
  }
  if (authResponse.roles.includes('Admin')) {
    return { isAdmin: true, isUser: false, isGuest: false };
  }
  if (authResponse.roles.includes('User')) {
    return { isAdmin: false, isUser: true, isGuest: false };
  }
  if (authResponse.roles.includes('Guest')) {
    return { isAdmin: false, isUser: false, isGuest: true };
  }
  return { isAdmin: false, isUser: false, isGuest: false };
};
