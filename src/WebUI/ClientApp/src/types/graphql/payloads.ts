import { Device, Group, User } from '../types';

// ########## Payloads ##########
/**
 * Base payload type
 */
export type Payload = {
  __typename?: 'AppQueries';
  message?: string;
  errors?: string[];
};

/**
 *   Devices payload
 */
export interface DevicesPayload extends Payload {
  devices?: Array<Device>;
}

/**
 *   Device payload
 */
export interface DevicePayload extends Payload {
  device?: Device;
}

/**
 * Identity paylaod
 */
export interface IdentityPayload extends Payload {
  token?: string;
  user?: User;
}

/**
 * Groups payload
 */
export interface GroupsPayload extends Payload {
  groups: Array<{ __typename?: 'Group' } & Group>;
}
