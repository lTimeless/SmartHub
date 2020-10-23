import {
  AuthResponse,
  LoginRequest,
  RegistrationRequest,
  ServerResponse,
  User,
  UserUpdateRequest
} from '@/types/types';
import { api } from '@/router/axios/axios';

const API_URL_ME = 'api/Identity/me';
const API_URL_LOGIN = 'api/Identity/login';
const API_URL_REGISTRATION = 'api/Identity/registration';

export const getMe = (): Promise<ServerResponse<User>> =>
  api.get<ServerResponse<User>>(API_URL_ME).then((res) => res.data);

export const putMe = (payload: UserUpdateRequest): Promise<ServerResponse<User>> =>
  api.put<ServerResponse<User>>(API_URL_ME, payload).then((res) => res.data);

export const postLogin = (payload: LoginRequest): Promise<ServerResponse<AuthResponse>> =>
  api.post<ServerResponse<AuthResponse>>(API_URL_LOGIN, payload).then((res) => res.data);

export const postRegistration = (payload: RegistrationRequest): Promise<ServerResponse<AuthResponse>> =>
  api.post<ServerResponse<AuthResponse>>(API_URL_REGISTRATION, payload).then((res) => res.data);
