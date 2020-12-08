import { IdentityPayload } from '@/types/types';
import { Roles } from '@/types/enums';
import JwtDecode from 'jwt-decode';
import router from '@/router';

type TokenPayload = {
  unique_name: string;
  jti: string;
  roles: string[] | string;
  nbf: number;
  exp: number;
  iat: number;
};
const numberThousand = 1000; // used for tokenpayload exp date conversion
// Storage keys
const LOCAL_STORAGE_TOKEN = 'token';

export const getStoreToken = (): string | null => {
  const storageToken = localStorage.getItem(LOCAL_STORAGE_TOKEN);
  if (storageToken === null) {
    return null;
  }
  return storageToken;
};

export const getToken = (): string | null => {
  const token = getStoreToken();
  return token === null ? null : token;
};

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
  const tokenPayload = JwtDecode(token) as TokenPayload;
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

export const getUserName = (): string => {
  const token = getToken();
  if (token === null) {
    return '';
  }
  const tokenPayload = JwtDecode(token) as TokenPayload;
  return tokenPayload.unique_name;
};

export const logout = (): void => {
  // const store = useStore();
  // store.dispatch(A_LOGOUT);
  clearStorage();
  router.push('/login');
};
