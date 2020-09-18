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

// Identity
export interface AuthResponse {
  token: string;
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
export interface User {
  userName: string;
  personInfo: string | null;
  personName: PersonName;
  roles: string[] | null;
  email: string | null;
  phoneNumber: string | null;
  lastModifiedAt: string;
  lastModifiedBy: string;
}

interface PersonName {
  firstName: string | null;
  middleName: string | null;
  lastName: string | null;
}

export interface UserUpdateRequest {
  userName: string;
  personInfo: string;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  newRole: string;
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
