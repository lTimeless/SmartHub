import gql from 'graphql-tag';
import * as Urql from '@urql/vue';
export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
};

export type Address = {
  __typename?: 'Address';
  street: Scalars['String'];
  city: Scalars['String'];
  state: Scalars['String'];
  country: Scalars['String'];
  zipCode: Scalars['String'];
};

export type AppConfig = {
  __typename?: 'AppConfig';
  configFilePath: Scalars['String'];
  applicationName?: Maybe<Scalars['String']>;
  configName?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
  address?: Maybe<Address>;
  isActive: Scalars['Boolean'];
  unitSystem?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
  downloadServerUrl?: Maybe<Scalars['String']>;
  baseFolderName?: Maybe<Scalars['String']>;
  configFolderName?: Maybe<Scalars['String']>;
  configFolderPath?: Maybe<Scalars['String']>;
  configFileName?: Maybe<Scalars['String']>;
  pluginFolderName?: Maybe<Scalars['String']>;
  pluginFolderPath?: Maybe<Scalars['String']>;
  logFolderName?: Maybe<Scalars['String']>;
  logFolderPath?: Maybe<Scalars['String']>;
  deleteXAmountAfterLimit?: Maybe<Scalars['Int']>;
  saveXLimit?: Maybe<Scalars['Int']>;
};

/** Main entrypoint for all mutations. */
export type AppMutations = {
  __typename?: 'AppMutations';
  login: IdentityPayload;
  registration: IdentityPayload;
  initializeApp: InitPayload;
  createGroup: GroupPayload;
  updateGroup: GroupPayload;
  createDevice: DevicePayload;
  updateDevice: DevicePayload;
  updateUser: UserPayload;
};

/** Main entrypoint for all mutations. */
export type AppMutationsLoginArgs = {
  input: LoginInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsRegistrationArgs = {
  input: RegistrationInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsInitializeAppArgs = {
  input: AppConfigInitInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsCreateGroupArgs = {
  input: CreateGroupInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsUpdateGroupArgs = {
  input: UpdateGroupInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsCreateDeviceArgs = {
  input: CreateDeviceInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsUpdateDeviceArgs = {
  input: UpdateDeviceInput;
};

/** Main entrypoint for all mutations. */
export type AppMutationsUpdateUserArgs = {
  input: UpdateUserInput;
};

/** Main entrypoint for all queries. */
export type AppQueries = {
  __typename?: 'AppQueries';
  groups: Array<Group>;
  groupsCount: Scalars['Int'];
  devices: Array<Device>;
  devicesCount: Scalars['Int'];
  me: IdentityPayload;
  appConfig: AppConfig;
  usersExist: Scalars['Boolean'];
  applicationIsActive: Scalars['Boolean'];
  scanNetworkDevices: Array<NetworkDevice>;
  setLightState: DeviceStatePayload;
};

/** Main entrypoint for all queries. */
export type AppQueriesGroupsArgs = {
  where?: Maybe<GroupFilterInput>;
  order?: Maybe<Array<GroupSortInput>>;
};

/** Main entrypoint for all queries. */
export type AppQueriesDevicesArgs = {
  where?: Maybe<DeviceFilterInput>;
  order?: Maybe<Array<DeviceSortInput>>;
};

/** Main entrypoint for all queries. */
export type AppQueriesScanNetworkDevicesArgs = {
  where?: Maybe<NetworkDeviceFilterInput>;
  order?: Maybe<Array<NetworkDeviceSortInput>>;
};

/** Main entrypoint for all queries. */
export type AppQueriesSetLightStateArgs = {
  input: DeviceLightStateInput;
};

export type BaseEntity = {
  __typename?: 'BaseEntity';
  setName: BaseEntity;
  setDescription: BaseEntity;
  id: Scalars['String'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdAt: Scalars['DateTime'];
  lastModifiedAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  lastModifiedBy: Scalars['String'];
};

export type BaseEntitySetNameArgs = {
  name: Scalars['String'];
};

export type BaseEntitySetDescriptionArgs = {
  description: Scalars['String'];
};

export type Company = {
  __typename?: 'Company';
  name: Scalars['String'];
  shortName: Scalars['String'];
};

export type Device = {
  __typename?: 'Device';
  setName: BaseEntity;
  setDescription: BaseEntity;
  ip: IpAddress;
  company: Company;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
  pluginName: Scalars['String'];
  pluginTypes: PluginTypes;
  groups: Array<Group>;
  id: Scalars['String'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdAt: Scalars['DateTime'];
  lastModifiedAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  lastModifiedBy: Scalars['String'];
  status?: Maybe<StatusResponseType>;
};

export type DeviceSetNameArgs = {
  name: Scalars['String'];
};

export type DeviceSetDescriptionArgs = {
  description: Scalars['String'];
};

export type DevicePayload = {
  __typename?: 'DevicePayload';
  device?: Maybe<Device>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type DeviceStatePayload = {
  __typename?: 'DeviceStatePayload';
  lightResponseType?: Maybe<LightResponseType>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type Group = {
  __typename?: 'Group';
  setName: BaseEntity;
  setDescription: BaseEntity;
  devices: Array<Device>;
  id: Scalars['String'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdAt: Scalars['DateTime'];
  lastModifiedAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  lastModifiedBy: Scalars['String'];
};

export type GroupSetNameArgs = {
  name: Scalars['String'];
};

export type GroupSetDescriptionArgs = {
  description: Scalars['String'];
};

export type GroupPayload = {
  __typename?: 'GroupPayload';
  group?: Maybe<Group>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type IdentityPayload = {
  __typename?: 'IdentityPayload';
  token?: Maybe<Scalars['String']>;
  user?: Maybe<User>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type InitPayload = {
  __typename?: 'InitPayload';
  appConfig?: Maybe<AppConfig>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type IpAddress = {
  __typename?: 'IpAddress';
  ipv4: Scalars['String'];
};

export type LightResponseType = {
  __typename?: 'LightResponseType';
  ison: Scalars['Boolean'];
  mode?: Maybe<Scalars['String']>;
  red: Scalars['Int'];
  green: Scalars['Int'];
  blue: Scalars['Int'];
  white: Scalars['Int'];
};

export type NetworkDevice = {
  __typename?: 'NetworkDevice';
  name?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
  ipv4?: Maybe<Scalars['String']>;
  ipv6?: Maybe<Scalars['String']>;
  hostname?: Maybe<Scalars['String']>;
  macAddress?: Maybe<Scalars['String']>;
};

export type PersonName = {
  __typename?: 'PersonName';
  firstName: Scalars['String'];
  middleName: Scalars['String'];
  lastName: Scalars['String'];
};

export type StatusResponseType = {
  __typename?: 'StatusResponseType';
  lights?: Maybe<Array<Maybe<LightResponseType>>>;
};

export type User = {
  __typename?: 'User';
  createdAt: Scalars['DateTime'];
  lastModifiedAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  lastModifiedBy: Scalars['String'];
  isFirstLogin: Scalars['Boolean'];
  personInfo: Scalars['String'];
  personName: PersonName;
  id?: Maybe<Scalars['String']>;
  userName?: Maybe<Scalars['String']>;
  normalizedUserName?: Maybe<Scalars['String']>;
  email?: Maybe<Scalars['String']>;
  normalizedEmail?: Maybe<Scalars['String']>;
  emailConfirmed: Scalars['Boolean'];
  passwordHash?: Maybe<Scalars['String']>;
  securityStamp?: Maybe<Scalars['String']>;
  concurrencyStamp?: Maybe<Scalars['String']>;
  phoneNumber?: Maybe<Scalars['String']>;
  phoneNumberConfirmed: Scalars['Boolean'];
  twoFactorEnabled: Scalars['Boolean'];
  lockoutEnd?: Maybe<Scalars['DateTime']>;
  lockoutEnabled: Scalars['Boolean'];
  accessFailedCount: Scalars['Int'];
};

export type UserError = {
  __typename?: 'UserError';
  message: Scalars['String'];
  code: AppErrorCodes;
};

export type UserPayload = {
  __typename?: 'UserPayload';
  user: User;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
};

export type AppConfigInitInput = {
  name?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
  autoDetectAddress: Scalars['Boolean'];
};

export type CompanyFilterInput = {
  and?: Maybe<Array<CompanyFilterInput>>;
  or?: Maybe<Array<CompanyFilterInput>>;
  name?: Maybe<StringOperationFilterInput>;
  shortName?: Maybe<StringOperationFilterInput>;
};

export type CompanySortInput = {
  name?: Maybe<SortEnumType>;
  shortName?: Maybe<SortEnumType>;
};

export type ComparableDateTimeOffsetOperationFilterInput = {
  eq?: Maybe<Scalars['DateTime']>;
  neq?: Maybe<Scalars['DateTime']>;
  in?: Maybe<Array<Scalars['DateTime']>>;
  nin?: Maybe<Array<Scalars['DateTime']>>;
  gt?: Maybe<Scalars['DateTime']>;
  ngt?: Maybe<Scalars['DateTime']>;
  gte?: Maybe<Scalars['DateTime']>;
  ngte?: Maybe<Scalars['DateTime']>;
  lt?: Maybe<Scalars['DateTime']>;
  nlt?: Maybe<Scalars['DateTime']>;
  lte?: Maybe<Scalars['DateTime']>;
  nlte?: Maybe<Scalars['DateTime']>;
};

export type ConnectionTypesOperationFilterInput = {
  eq?: Maybe<ConnectionTypes>;
  neq?: Maybe<ConnectionTypes>;
  in?: Maybe<Array<ConnectionTypes>>;
  nin?: Maybe<Array<ConnectionTypes>>;
};

export type CreateDeviceInput = {
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  ipv4: Scalars['String'];
  companyName: Scalars['String'];
  pluginName: Scalars['String'];
  groupName?: Maybe<Scalars['String']>;
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
};

export type CreateGroupInput = {
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
};

export type DeviceFilterInput = {
  and?: Maybe<Array<DeviceFilterInput>>;
  or?: Maybe<Array<DeviceFilterInput>>;
  ip?: Maybe<IpAddressFilterInput>;
  company?: Maybe<CompanyFilterInput>;
  primaryConnection?: Maybe<ConnectionTypesOperationFilterInput>;
  secondaryConnection?: Maybe<ConnectionTypesOperationFilterInput>;
  pluginName?: Maybe<StringOperationFilterInput>;
  pluginTypes?: Maybe<PluginTypesOperationFilterInput>;
  groups?: Maybe<ListFilterInputTypeOfGroupFilterInput>;
  id?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  createdAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  lastModifiedAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  createdBy?: Maybe<StringOperationFilterInput>;
  lastModifiedBy?: Maybe<StringOperationFilterInput>;
};

export type DeviceLightStateInput = {
  deviceId: Scalars['String'];
  setLight: Scalars['Boolean'];
};

export type DeviceSortInput = {
  ip?: Maybe<IpAddressSortInput>;
  company?: Maybe<CompanySortInput>;
  primaryConnection?: Maybe<SortEnumType>;
  secondaryConnection?: Maybe<SortEnumType>;
  pluginName?: Maybe<SortEnumType>;
  pluginTypes?: Maybe<SortEnumType>;
  id?: Maybe<SortEnumType>;
  name?: Maybe<SortEnumType>;
  description?: Maybe<SortEnumType>;
  createdAt?: Maybe<SortEnumType>;
  lastModifiedAt?: Maybe<SortEnumType>;
  createdBy?: Maybe<SortEnumType>;
  lastModifiedBy?: Maybe<SortEnumType>;
};

export type GroupFilterInput = {
  and?: Maybe<Array<GroupFilterInput>>;
  or?: Maybe<Array<GroupFilterInput>>;
  devices?: Maybe<ListFilterInputTypeOfDeviceFilterInput>;
  id?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  createdAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  lastModifiedAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  createdBy?: Maybe<StringOperationFilterInput>;
  lastModifiedBy?: Maybe<StringOperationFilterInput>;
};

export type GroupSortInput = {
  id?: Maybe<SortEnumType>;
  name?: Maybe<SortEnumType>;
  description?: Maybe<SortEnumType>;
  createdAt?: Maybe<SortEnumType>;
  lastModifiedAt?: Maybe<SortEnumType>;
  createdBy?: Maybe<SortEnumType>;
  lastModifiedBy?: Maybe<SortEnumType>;
};

export type IpAddressFilterInput = {
  and?: Maybe<Array<IpAddressFilterInput>>;
  or?: Maybe<Array<IpAddressFilterInput>>;
  ipv4?: Maybe<StringOperationFilterInput>;
};

export type IpAddressSortInput = {
  ipv4?: Maybe<SortEnumType>;
};

export type ListFilterInputTypeOfDeviceFilterInput = {
  all?: Maybe<DeviceFilterInput>;
  none?: Maybe<DeviceFilterInput>;
  some?: Maybe<DeviceFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfGroupFilterInput = {
  all?: Maybe<GroupFilterInput>;
  none?: Maybe<GroupFilterInput>;
  some?: Maybe<GroupFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type LoginInput = {
  userName: Scalars['String'];
  password: Scalars['String'];
};

export type NetworkDeviceFilterInput = {
  and?: Maybe<Array<NetworkDeviceFilterInput>>;
  or?: Maybe<Array<NetworkDeviceFilterInput>>;
  name?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  ipv4?: Maybe<StringOperationFilterInput>;
  ipv6?: Maybe<StringOperationFilterInput>;
  hostname?: Maybe<StringOperationFilterInput>;
  macAddress?: Maybe<StringOperationFilterInput>;
};

export type NetworkDeviceSortInput = {
  name?: Maybe<SortEnumType>;
  description?: Maybe<SortEnumType>;
  ipv4?: Maybe<SortEnumType>;
  ipv6?: Maybe<SortEnumType>;
  hostname?: Maybe<SortEnumType>;
  macAddress?: Maybe<SortEnumType>;
};

export type PluginTypesOperationFilterInput = {
  eq?: Maybe<PluginTypes>;
  neq?: Maybe<PluginTypes>;
  in?: Maybe<Array<PluginTypes>>;
  nin?: Maybe<Array<PluginTypes>>;
};

export type RegistrationInput = {
  userName: Scalars['String'];
  password: Scalars['String'];
  role: Scalars['String'];
};

export type StringOperationFilterInput = {
  and?: Maybe<Array<StringOperationFilterInput>>;
  or?: Maybe<Array<StringOperationFilterInput>>;
  eq?: Maybe<Scalars['String']>;
  neq?: Maybe<Scalars['String']>;
  contains?: Maybe<Scalars['String']>;
  ncontains?: Maybe<Scalars['String']>;
  in?: Maybe<Array<Maybe<Scalars['String']>>>;
  nin?: Maybe<Array<Maybe<Scalars['String']>>>;
  startsWith?: Maybe<Scalars['String']>;
  nstartsWith?: Maybe<Scalars['String']>;
  endsWith?: Maybe<Scalars['String']>;
  nendsWith?: Maybe<Scalars['String']>;
};

export type UpdateDeviceInput = {
  id: Scalars['String'];
  name?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
  ipv4?: Maybe<Scalars['String']>;
  groupName?: Maybe<Scalars['String']>;
  primaryConnection?: Maybe<ConnectionTypes>;
  secondaryConnection?: Maybe<ConnectionTypes>;
};

export type UpdateGroupInput = {
  id: Scalars['String'];
  name?: Maybe<Scalars['String']>;
  description?: Maybe<Scalars['String']>;
};

export type UpdateUserInput = {
  userId: Scalars['String'];
  userName?: Maybe<Scalars['String']>;
  personInfo?: Maybe<Scalars['String']>;
  firstName?: Maybe<Scalars['String']>;
  middleName?: Maybe<Scalars['String']>;
  lastName?: Maybe<Scalars['String']>;
  email?: Maybe<Scalars['String']>;
  phoneNumber?: Maybe<Scalars['String']>;
  newRole?: Maybe<Scalars['String']>;
};

export enum AppErrorCodes {
  ServerError = 'SERVER_ERROR',
  NotFound = 'NOT_FOUND',
  NotCreated = 'NOT_CREATED',
  NotUpdated = 'NOT_UPDATED',
  Exists = 'EXISTS',
  NoHome = 'NO_HOME',
  NotSet = 'NOT_SET',
  NotAuthorized = 'NOT_AUTHORIZED',
  IsEmpty = 'IS_EMPTY',
  IsSubGroup = 'IS_SUB_GROUP'
}

export enum ApplyPolicy {
  BeforeResolver = 'BEFORE_RESOLVER',
  AfterResolver = 'AFTER_RESOLVER'
}

export enum ConnectionTypes {
  None = 'NONE',
  Http = 'HTTP',
  Mqtt = 'MQTT'
}

export enum PluginTypes {
  None = 'NONE',
  Base = 'BASE',
  Mock = 'MOCK',
  Door = 'DOOR',
  Light = 'LIGHT',
  Ht = 'HT',
  Sensor = 'SENSOR',
  Rgb = 'RGB'
}

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type GetMeQueryVariables = Exact<{ [key: string]: never }>;

export type GetMeQuery = { __typename?: 'AppQueries' } & {
  me: { __typename?: 'IdentityPayload' } & {
    user?: Maybe<
      { __typename?: 'User' } & Pick<
        User,
        | 'id'
        | 'userName'
        | 'createdAt'
        | 'createdBy'
        | 'lastModifiedAt'
        | 'lastModifiedBy'
        | 'phoneNumber'
        | 'personInfo'
        | 'email'
      > & {
          personName: { __typename?: 'PersonName' } & Pick<
            PersonName,
            'firstName' | 'lastName' | 'middleName'
          >;
        }
    >;
  };
};

export const GetMeDocument = gql`
  query GetMe {
    me {
      user {
        id
        userName
        createdAt
        createdBy
        lastModifiedAt
        lastModifiedBy
        phoneNumber
        personInfo
        email
        personName {
          firstName
          lastName
          middleName
        }
      }
    }
  }
`;
