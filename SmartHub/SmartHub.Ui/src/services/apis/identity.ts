import {
  IdentityPayload,
  LoginInput,
  RegistrationInput,
  Payload,
  User,
  UpdateUserInput
} from '@/types/types';
import { api } from '@/router/axios/axios';

const API_URL_ME = 'api/Identity/me';
const API_URL_LOGIN = 'api/Identity/login';
const API_URL_REGISTRATION = 'api/Identity/registration';

// export const getMe = (): Promise<Payload<User>> =>
//   api.get<Payload<User>>(API_URL_ME).then((res) => res.data);
//
// export const putMe = (payload: UpdateUserInput): Promise<Payload<User>> =>
//   api.put<Payload<User>>(API_URL_ME, payload).then((res) => res.data);
//
// export const postLogin = (payload: LoginInput): Promise<Payload<IdentityPayload>> =>
//   api.post<Payload<IdentityPayload>>(API_URL_LOGIN, payload).then((res) => res.data);
//
// export const postRegistration = (payload: RegistrationInput): Promise<Payload<IdentityPayload>> =>
//   api.post<Payload<IdentityPayload>>(API_URL_REGISTRATION, payload).then((res) => res.data);
