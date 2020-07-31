import { AuthResponse, Home, User } from '@/types/types';

export interface RootState {
  version: number;
}

export interface UserState {
  user: User | null;
}

export interface HomeState {
  home: Home | null;
}

export interface AuthState {
  authResponse: AuthResponse | null;
  isSignInBtnClicked: boolean;
}
