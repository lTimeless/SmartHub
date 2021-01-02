import { Roles } from '@/types/enums';
import jwtDecode from 'jwt-decode';

type TokenPayload = {
  unique_name: string;
  jti: string;
  roles: string[] | string;
  nbf: number;
  exp: number;
  iat: number;
};
const numberThousand = 1000; // used for tokenpayload "exp date" conversion
// Storage keys
const LOCAL_STORAGE_TOKEN = 'token';

export const getToken = (): string | null => localStorage.getItem(LOCAL_STORAGE_TOKEN);

export const isAuthenticated = (): boolean => getToken() !== null;

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

export const clearStorage = (): void => {
  localStorage.removeItem(LOCAL_STORAGE_TOKEN);
};

// export const getRefreshToken = (): string | null => localStorage.getItem(LOCAL_STORAGE_REFRESH_TOKEN);

export const getUserRoles = (): Roles => {
  const token = getToken();
  if (token == null) {
    return Roles.None;
  }
  const tokenPayload = jwtDecode(token) as TokenPayload;
  if (Date.now() >= tokenPayload.exp * numberThousand) {
    console.log(tokenPayload.exp * numberThousand, Date.now());
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
