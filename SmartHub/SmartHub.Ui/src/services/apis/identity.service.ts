import { AuthResponse, LoginRequest, RegistrationRequest, ServerResponse, User } from '@/types/types';
import { Api } from '@/router/axios/axios';

const API_URL_WHOAMI = 'api/Identity/whoami';
const API_URL_LOGIN = 'api/Identity/login';
const API_URL_REGISTRATION = 'api/Identity/registration';

export const getWhoAmI = (): Promise<ServerResponse<User>> =>
  Api()
    .get<ServerResponse<User>>(API_URL_WHOAMI)
    .then((res) => res.data);

export const postLogin = (payload: LoginRequest): Promise<ServerResponse<AuthResponse>> =>
  Api()
    .post<ServerResponse<AuthResponse>>(API_URL_LOGIN, payload)
    .then((res) => res.data);

export const postRegistration = (payload: RegistrationRequest): Promise<ServerResponse<AuthResponse>> =>
  Api()
    .post<ServerResponse<AuthResponse>>(API_URL_REGISTRATION, payload)
    .then((res) => res.data);
