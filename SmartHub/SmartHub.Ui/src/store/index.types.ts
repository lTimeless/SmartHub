import { AuthResponse, Home, User } from '@/types/types';

export interface RootState {
  version: number;
  auth: AuthState;
}

export interface UserState {
  user: User | null;
}

export interface HomeState {
  home: Home | null;
}

export interface AuthState {
  authResponse: AuthResponse | null;
  regStepIndex: number;
  isSignInBtnClicked: boolean;
}
