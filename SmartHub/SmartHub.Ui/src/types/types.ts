import { ConnectionTypes, PluginTypes } from './enums';

interface BaseEntity {
  id: string;
  createdBy: string;
  createdAt: string;
  lastModifiedAt: string;
  lastModifiedBy: string;
  name: string;
  description?: string;
}

// ########## Interfaces ##########
// ServiceResponse
export interface ServerResponse<T> {
  data?: T;
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
export interface Home extends BaseEntity {
  settings?: Setting[];
  groups?: Group[];
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
export interface Group extends BaseEntity {
  devices?: Device[];
}

// GroupCreateRequest
export interface GroupCreateRequest {
  name: string;
  description: string;
}

export interface GroupUpdateRequest {
  id: string;
  name?: string;
  description?: string;
  devices?: Device[];
}

// Device
export interface Device extends BaseEntity {
  company: Company;
  ip: IpAddress;
  pluginName: string;
  pluginTypes: PluginTypes;
  primaryCOnnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
}

interface Company {
  name: string;
  shortName: string;
}
interface IpAddress {
  ipv4: string;
}

export interface DeviceCreateRequest {
  groupId: string;
  name: string;
  description?: string;
  ipv4: string;
  companyName: string;
  pluginName: string;
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection?: ConnectionTypes;
}

// Setting
export interface Setting extends BaseEntity {
  isActive: boolean;
  isDefault: boolean;
  pluginpath: string;
  filepath: string;
}

// ########## Types ##########
export type ModifiedEvent = {
  id: string;
  eventType: string;
  data: string;
};
