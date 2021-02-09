import { ConnectionTypes } from '../enums';

// ########## Inputs ##########
/**
 * AppConfig input
 */
export interface AppConfigInitInput {
  name?: string;
  description?: string;
  autoDetectAddress: boolean;
}

/**
 * User inputs
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
 * Device inputs
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
 * Group inputs
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
 * Registration Input
 */
export interface RegistrationInput {
  userName: string;
  password: string;
  role: string;
}

/**
 * Login Input
 */
export interface LoginInput {
  userName: string;
  password: string;
}
