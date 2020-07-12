import { LoginResponse, RegistrationResponse, ServiceResponse, User } from '@/types/types';

export interface RootState {
  version: number;
}

export interface UserState {
  user?: User;
  loginResponse?: ServiceResponse<LoginResponse>;
  registrationResponse?: ServiceResponse<RegistrationResponse>;
}
