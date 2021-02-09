import { AppErrorCodes } from '../enums';
import { AppConfig, Device, Group, User } from '../types';

// ########## Payloads ##########
/**
 * Base payload type
 */
export type UserError = {
  __typename?: 'UserError';
  message: string;
  code: AppErrorCodes;
};

export type Payload = {
  message?: string;
  errors?: Array<UserError>;
};

/**
 *   Init app
 */
export interface InitPayload extends Payload {
  __typename?: 'InitPayload';
  appConfig?: AppConfig;
}

/**
 *   Devices
 */
export interface DevicesPayload extends Payload {
  __typename?: 'DevicesPayload';
  devices?: Array<Device>;
}

/**
 *   Device
 */
export interface DevicePayload extends Payload {
  __typename?: 'DevicePayload';
  device?: Device;
}

/**
 * Identity
 */
export interface IdentityPayload extends Payload {
  __typename?: 'IdentityPayload';
  token?: string;
  user?: User;
}

/**
 * Group
 */
export interface GroupsPayload extends Payload {
  __typename?: 'GroupsPayload';
  groups: Array<{ __typename?: 'Group' } & Group>;
}

/**
 * User
 */
export interface UserPayload extends Payload {
  __typename?: 'UserPayload';
  user: User;
}
