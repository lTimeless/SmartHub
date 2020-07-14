import { AuthResponse, Home, User } from '@/types/types';

export interface RootState {
  version: number;
}

export interface UserState {
  user?: User;
  authResponse?: AuthResponse;
}

export interface HomeState {
  home?: Home;
}
