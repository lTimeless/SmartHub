import { AxiosRequestConfig } from 'axios';
import { AuthResponse } from '@/types/types';
import { Roles } from '@/types/enums';

// Storage keys
const LOCAL_STORAGE_TOKEN = 'token';
const LOCAL_STORAGE_AUTHRESPONSE = 'authResponse';
const LOCAL_STORAGE_REFRESH_TOKEN = 'refresh_token';

const getToken = (): string | null => localStorage.getItem(LOCAL_STORAGE_TOKEN);

export const isAuthenticated = (): boolean => getToken() !== null;

export const getAuthentication = (): AxiosRequestConfig => ({
  headers: { Authorization: `Bearer ${getToken()}` }
});

// TODO: logic will be implemented later, when refreshToken will be added
// public static getNewToken(): Promise<string> {
//   return new Promise((resolve, reject) => {
//     axios
//       .post(ApiUrlService.refreshToken(), { refresh_token: this.getRefreshToken() })
//       .then((response) => {
//         this.storeToken(response.data.token);
//         this.storeRefreshToken(response.data.refresh_token);
//
//         resolve(response.data.token);
//       })
//       .catch((error) => {
//         reject(error);
//       });
//   });
// }

export const storeToken = (token: string): void => {
  localStorage.setItem(LOCAL_STORAGE_TOKEN, token);
};

export const storeAuthResponse = (response: AuthResponse): void => {
  localStorage.setItem(LOCAL_STORAGE_AUTHRESPONSE, JSON.stringify(response));
};

export const getAuthResponse = (): AuthResponse | null => {
  const storage = localStorage.getItem(LOCAL_STORAGE_AUTHRESPONSE);
  if (storage === null) {
    return null;
  }
  return JSON.parse(storage) as AuthResponse;
};

export const storeRefreshToken = (refreshToken: string): void => {
  localStorage.setItem(LOCAL_STORAGE_REFRESH_TOKEN, refreshToken);
};

export const clearStorage = (): void => {
  localStorage.removeItem(LOCAL_STORAGE_TOKEN);
  localStorage.removeItem(LOCAL_STORAGE_REFRESH_TOKEN);
};

// export const getRefreshToken = (): string | null => localStorage.getItem(LOCAL_STORAGE_REFRESH_TOKEN);

export const getUserRole = (): Roles => {
  const authResponse = getAuthResponse();
  if (authResponse == null) {
    return Roles.None;
  }
  if (authResponse.roles.includes('Admin')) {
    return Roles.Admin;
  }
  if (authResponse.roles.includes('User')) {
    return Roles.User;
  }
  if (authResponse.roles.includes('Guest')) {
    return Roles.Guest;
  }
  return Roles.None;
};
