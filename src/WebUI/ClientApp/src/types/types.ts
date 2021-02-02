import { ConnectionTypes, PluginTypes } from './enums';

// ########## Entities ##########
/*
  Application config
 */
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

/*
  Base entity
 */
interface BaseEntity {
  id: string;
  createdBy: string;
  createdAt: string;
  lastModifiedAt: string;
  lastModifiedBy: string;
  name: string;
  description?: string;
}

interface PersonName {
  firstName: string | null;
  middleName: string | null;
  lastName: string | null;
}

export interface User {
  id: string;
  userName: string;
  personInfo: string | null;
  personName: PersonName;
  roles: string[] | null;
  email: string | null;
  phoneNumber: string | null;
  lastModifiedAt: string;
  lastModifiedBy: string;
}

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

export interface Group extends BaseEntity {
  devices?: Device[];
  isSubGroup: boolean;
  subGroups?: Group[];
}

// ########## GraphQl ##########
/*
  GraphQl payload type
 */
export interface Payload {
  message?: string;
  errors?: string[];
}

// Identity Inputs, Payloads
export interface IdentityPayload extends Payload {
  token?: string;
  user?: User;
}

export interface LoginInput {
  userName: string;
  password: string;
}

export interface RegistrationInput {
  userName: string;
  password: string;
  role: string;
}

// User Inputs
export interface UpdateUserInput {
  userId: string;
  userName?: string;
  personInfo?: string;
  firstName?: string;
  middleName?: string;
  lastName?: string;
  email?: string;
  phoneNumber?: string;
  newRole?: string;
}

// Device Inputs
export interface CreateDeviceInput {
  groupName?: string;
  name: string;
  description?: string;
  ipv4: string;
  companyName: string;
  pluginName: string;
  pluginTypes: string;
  primaryConnection: string;
  secondaryConnection: string;
}

export interface UpdateDeviceInput {
  id: string;
  groupName?: string;
  name?: string;
  description?: string;
  ipv4?: string;
  primaryConnection?: ConnectionTypes;
  secondaryConnection?: ConnectionTypes;
}

// Group Inputs
export interface CreateGroupInput {
  name: string;
  description?: string;
  parentGroupId?: string;
}

export interface UpdateGroupInput {
  id: string;
  name?: string;
  description?: string;
  isSubGroup?: boolean;
  devices?: Device[];
}

// AppConfig Inputs
export interface AppConfigInitInput {
  name?: string;
  description?: string;
  autoDetectAddress: boolean;
}
