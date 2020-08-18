import { AxiosRequestConfig } from 'axios';
import { AuthResponse } from '@/types/types';
import { Roles } from '@/types/enums';
import JwtDecode from 'jwt-decode';

type TokenPayload = { unique_name: string; jti: string; roles: string[] | string; nbf: number; exp: number; iat: number };

// Storage keys
const LOCAL_STORAGE_AUTH_RESPONSE = 'authResponse';

export const getAuthResponse = (): AuthResponse | null => {
  const storage = localStorage.getItem(LOCAL_STORAGE_AUTH_RESPONSE);
  if (storage === null) {
    return null;
  }
  return JSON.parse(storage) as AuthResponse;
};

export const getToken = (): string | null => {
  const auth = getAuthResponse();
  return auth === null ? null : auth.token;
};

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

export const storeAuthResponse = (response: AuthResponse): void => {
  localStorage.setItem(LOCAL_STORAGE_AUTH_RESPONSE, JSON.stringify(response));
};

export const clearStorage = (): void => {
  localStorage.removeItem(LOCAL_STORAGE_AUTH_RESPONSE);
};

// export const getRefreshToken = (): string | null => localStorage.getItem(LOCAL_STORAGE_REFRESH_TOKEN);

export const getUserRole = (): Roles => {
  const authResponse = getAuthResponse();
  if (authResponse == null) {
    return Roles.None;
  }
  const tokenPayload = JwtDecode(authResponse.token) as TokenPayload;
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
