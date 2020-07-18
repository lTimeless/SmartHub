// ServiceResponse
export interface Response<T> {
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

export interface HomeCreateRequest {
  name: string;
  description?: string;
}

// User
export interface User {
  username: string;
}

// Home
export interface Home {
  id: string;
  createdAt: string;
  modifiedDate: string;
  name: string;
  description?: string;
  settings?: Setting[];
}

// Group
export interface Group {
  name: string;
}

// Device
export interface Device {
  name: string;
}

// Setting
export interface Setting {
  name: string;
}
