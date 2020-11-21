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

export interface ServerActivity {
  id: string;
  dateTime: string;
  userName: string;
  message: string;
  executionTime: number;
  successfulRequest?: boolean;
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
interface PersonName {
  firstName: string | null;
  middleName: string | null;
  lastName: string | null;
}

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

// Device
interface Company {
  name: string;
  shortName: string;
}
interface IpAddress {
  ipv4: string;
}

export interface Device extends BaseEntity {
  company: Company;
  ip: IpAddress;
  pluginName: string;
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
}

export interface DeviceCreateRequest {
  groupName?: string;
  name: string;
  description?: string;
  ipv4: string;
  companyName: string;
  pluginName: string;
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
}

export interface DeviceUpdateRequest {
  id: string;
  groupName?: string;
  name?: string;
  description?: string;
  ipv4?: string;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
}

// Group
export interface Group extends BaseEntity {
  devices?: Device[];
  isSubGroup: boolean;
  subGroups?: Group[];
}

export interface GroupCreateRequest {
  name: string;
  description: string;
  parentGroupId?: string;
  isSubGroup?: boolean;
}

export interface GroupUpdateRequest {
  id: string;
  name?: string;
  description?: string;
  isSubGroup?: boolean;
  devices?: Device[];
}

// Application Config
export interface AppConfig {
  applicationName?: string;
  configName?: string;
  description?: string;
  address?: Address;
  isActive: boolean;
  unitSystem?: string;
  timeZone?: string;
  downloadServerUrl?: string;
  baseFolderName?: string;
  configFolderName?: string;
  configFolderPath?: string;
  configFileName?: string;
  configFilePath?: string;
  pluginFolderName?: string;
  pluginFolderPath?: string;
  logFolderName?: string;
  logFolderPath?: string;
  deleteXAmountAfterLimit?: string;
  saveXLimit?: string;
}

interface Address {
  street: string;
  city: string;
  state: string;
  country: string;
  zipCode: string;
}

export interface AppConfigInitRequest {
  name: string;
  description: string | null;
  autoDetectAddress: boolean;
}
