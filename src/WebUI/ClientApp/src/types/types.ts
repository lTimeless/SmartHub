import { ConnectionTypes, PluginTypes } from './enums';
// ########## Entities ##########

/**
 * Address
 */
interface Address {
  __typename?: 'Address';
  street: string;
  city: string;
  state: string;
  country: string;
  zipCode: string;
}

/*
  Application config
 */
export interface AppConfig {
  __typename?: 'AppConfig';
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

export interface PersonName {
  __typename?: 'PersonName';
  firstName?: string;
  middleName?: string;
  lastName?: string;
}

export interface User extends BaseEntity {
  __typename?: 'User';
  id: string;
  userName: string;
  personInfo?: string;
  personName: PersonName;
  roles?: string[];
  email?: string;
  phoneNumber?: string;
}

export interface Company {
  __typename?: 'Company';
  name: string;
  shortName: string;
}
export interface IpAddress {
  __typename?: 'IpAddress';
  ipv4: string;
}

export interface Device extends BaseEntity {
  __typename?: 'Device';
  company: Company;
  ip: IpAddress;
  pluginName: string;
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
  status?: LightState;
}

export interface Group extends BaseEntity {
  __typename?: 'Group';
  devices?: Device[];
  isSubGroup: boolean;
  subGroups?: Group[];
}

// ########## Device/Plugin Types ##########
export type LightState = {
  lights?: LightResponse[];
};

export type LightResponse = {
  __typename?: 'LightResponse';
  ison: boolean;
  mode?: string;
  red: number;
  green: number;
  blue: number;
  white: number;
};
