// ServiceResponse
export interface ServiceResponse<T> {
  data: T;
  success: boolean;
  message: string;
  errors: string[];
}

// Auth
export interface AuthResponse {
  token: string;
  expiresAt: string; // TODO: DateTime
  username: string;
  roles: string[];
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegistrationRequest {
  username: string;
  password: string;
  role: string;
}

// User
export interface User {}

// Home
export interface Home {}

// Group
export interface Group {}

// Device
export interface Device {}

// Setting
export interface Setting {}
