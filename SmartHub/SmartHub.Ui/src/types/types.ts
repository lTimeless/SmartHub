// ########## Interfaces ##########
// ServiceResponse
export interface ServerResponse<T> {
  data: T | null;
  success: boolean;
  message: string;
  errors: string[];
}

export interface ServerEvent {
  id: string;
  eventType: string;
  data: object;
}

export interface ServerLog {
  id: string;
  timestamp: string;
  level: string;
  message: string;
  exception: string;
}

// Auth
export interface AuthResponse {
  token: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegistrationRequest {
  username: string;
  firstname: string;
  lastname: string;
  password: string;
  role: string;
}

export interface HomeCreateRequest {
  name: string;
  description: string | null;
  autoDetectAddress: boolean;
}

export interface HomeUpdateRequest {
  name: string | null;
  description: string | null;
  userName: string | null;
  settingName: string | null;
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

// ########## Types ##########
export type ModifiedEvent = {
  id: string;
  eventType: string;
  data: string;
};
