import { AuthResponse, Home, User } from '@/types/types';

export interface RootState {
  version: number;
  authModule: AuthState;
  homeModule: HomeState;
  userModule: UserState;
}

/*
  State for all users without myself
*/
export interface UserState {
  user: User | null;
}

/*
  State for the home data
*/
export interface HomeState {
  home: Home | null;
}

/*
  State for all identity information with my own user data
*/
export interface AuthState {
  authResponse: AuthResponse | null;
  Me: User | null;
}
