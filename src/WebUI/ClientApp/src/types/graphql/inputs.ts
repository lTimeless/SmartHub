import { ConnectionTypes } from '../enums';

// ########## Inputs ##########
/**
 * AppConfig
 */
export interface AppConfigInitInput {
  name?: string;
  description?: string;
  autoDetectAddress: boolean;
}

/**
 * User
 */
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

/**
 * Device
 */
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

/**
 * Device Light State
 */
export type DeviceLightStateInput = {
  deviceId: string;
  setLight: boolean;
};

/**
 * Group
 */
export interface CreateGroupInput {
  name: string;
  description?: string;
  parentGroupId?: string;
}

export interface UpdateGroupInput {
  id: string;
  name?: string;
  description?: string;
}

/**
 * Registration
 */
export interface RegistrationInput {
  userName: string;
  password: string;
  role: string;
}

/**
 * Login
 */
export interface LoginInput {
  userName: string;
  password: string;
}
