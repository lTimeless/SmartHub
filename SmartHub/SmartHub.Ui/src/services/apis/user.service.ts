import { AuthResponse, LoginRequest, RegistrationRequest, ServerResponse } from '@/types/types';
import axios from '@/router/axios/axios';

const API_URL_LOGIN = 'api/Identity/login';
const API_URL_REGISTRATION = 'api/Identity/registration';

export const postLogin = (payload: LoginRequest): Promise<ServerResponse<AuthResponse>> =>
  axios.post<ServerResponse<AuthResponse>>(API_URL_LOGIN, payload).then((res) => res.data);

export const postRegistration = (payload: RegistrationRequest): Promise<ServerResponse<AuthResponse>> =>
  axios.post<ServerResponse<AuthResponse>>(API_URL_REGISTRATION, payload).then((res) => res.data);
