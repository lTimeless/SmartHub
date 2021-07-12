export type Maybe<T> = T | null | undefined;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export interface Scalars {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
}

export interface Address {
  __typename?: 'Address';
  city: Scalars['String'];
  country: Scalars['String'];
  state: Scalars['String'];
  street: Scalars['String'];
  zipCode: Scalars['String'];
}

export interface AppConfig {
  __typename?: 'AppConfig';
  address?: Maybe<Address>;
  applicationName?: Maybe<Scalars['String']>;
  baseFolderName?: Maybe<Scalars['String']>;
  configFileName?: Maybe<Scalars['String']>;
  configFilePath: Scalars['String'];
  configFolderName?: Maybe<Scalars['String']>;
  configFolderPath?: Maybe<Scalars['String']>;
  configName?: Maybe<Scalars['String']>;
  deleteXAmountAfterLimit?: Maybe<Scalars['Int']>;
  description?: Maybe<Scalars['String']>;
  downloadServerUrl?: Maybe<Scalars['String']>;
  firstStartUp: Scalars['Boolean'];
  isActive: Scalars['Boolean'];
  logFilePath: Scalars['String'];
  logFolderName?: Maybe<Scalars['String']>;
  logFolderPath?: Maybe<Scalars['String']>;
  pluginFolderName?: Maybe<Scalars['String']>;
  pluginFolderPath?: Maybe<Scalars['String']>;
  saveXLimit?: Maybe<Scalars['Int']>;
  timeZone?: Maybe<Scalars['String']>;
  unitSystem?: Maybe<Scalars['String']>;
}

export interface AppConfigInitInput {
  autoDetectAddress: Scalars['Boolean'];
  description?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
}

export enum AppErrorCodes {
  Exists = 'EXISTS',
  IsEmpty = 'IS_EMPTY',
  IsSubGroup = 'IS_SUB_GROUP',
  NotAuthorized = 'NOT_AUTHORIZED',
  NotCreated = 'NOT_CREATED',
  NotFound = 'NOT_FOUND',
  NotSet = 'NOT_SET',
  NotUpdated = 'NOT_UPDATED',
  NoHome = 'NO_HOME',
  ServerError = 'SERVER_ERROR'
}

/** Main entrypoint for all mutations. */
export interface AppMutations {
  __typename?: 'AppMutations';
  createDevice: DevicePayload;
  createGroup: GroupPayload;
  initializeApp: InitPayload;
  login: IdentityPayload;
  refreshTokens: IdentityPayload;
  registration: IdentityPayload;
  updateDevice: DevicePayload;
  updateGroup: GroupPayload;
  updateUser: UserPayload;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsCreateDeviceArgs {
  input: CreateDeviceInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsCreateGroupArgs {
  input: CreateGroupInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsInitializeAppArgs {
  input: AppConfigInitInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsLoginArgs {
  input: LoginInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsRegistrationArgs {
  input: RegistrationInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsUpdateDeviceArgs {
  input: UpdateDeviceInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsUpdateGroupArgs {
  input: UpdateGroupInput;
}

/** Main entrypoint for all mutations. */
export interface AppMutationsUpdateUserArgs {
  input: UpdateUserInput;
}

/** Main entrypoint for all queries. */
export interface AppQueries {
    __typename?: 'AppQueries';
    appConfig: AppConfig;
    applicationIsActive: Scalars['Boolean'];
    devices: Array<Device>;
    devicesCount: Scalars['Int'];
    getMe: IdentityPayload;
    groups: Array<Group>;
    groupsCount: Scalars['Int'];
    logout: IdentityPayload;
    scanNetworkDevices: Array<NetworkDevice>;
    setLightState: DeviceStatePayload;
    usersExist: Scalars['Boolean'];
}

/** Main entrypoint for all queries. */
export interface AppQueriesDevicesArgs {
  order?: Maybe<Array<DeviceSortInput>>;
  where?: Maybe<DeviceFilterInput>;
}

/** Main entrypoint for all queries. */
export interface AppQueriesGroupsArgs {
  order?: Maybe<Array<GroupSortInput>>;
  where?: Maybe<GroupFilterInput>;
}

/** Main entrypoint for all queries. */
export interface AppQueriesScanNetworkDevicesArgs {
  order?: Maybe<Array<NetworkDeviceSortInput>>;
  where?: Maybe<NetworkDeviceFilterInput>;
}

/** Main entrypoint for all queries. */
export interface AppQueriesSetLightStateArgs {
  input: DeviceLightStateInput;
}

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER'
}

export interface BaseEntity {
  __typename?: 'BaseEntity';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  lastModifiedAt: Scalars['DateTime'];
  lastModifiedBy: Scalars['String'];
  name: Scalars['String'];
  setDescription: BaseEntity;
  setName: BaseEntity;
}

export interface BaseEntitySetDescriptionArgs {
  description: Scalars['String'];
}

export interface BaseEntitySetNameArgs {
  name: Scalars['String'];
}

export interface Company {
  __typename?: 'Company';
  name: Scalars['String'];
  shortName: Scalars['String'];
}

export interface CompanyFilterInput {
  and?: Maybe<Array<CompanyFilterInput>>;
  name?: Maybe<StringOperationFilterInput>;
  or?: Maybe<Array<CompanyFilterInput>>;
  shortName?: Maybe<StringOperationFilterInput>;
}

export interface CompanySortInput {
  name?: Maybe<SortEnumType>;
  shortName?: Maybe<SortEnumType>;
}

export interface ComparableDateTimeOffsetOperationFilterInput {
  eq?: Maybe<Scalars['DateTime']>;
  gt?: Maybe<Scalars['DateTime']>;
  gte?: Maybe<Scalars['DateTime']>;
  in?: Maybe<Array<Scalars['DateTime']>>;
  lt?: Maybe<Scalars['DateTime']>;
  lte?: Maybe<Scalars['DateTime']>;
  neq?: Maybe<Scalars['DateTime']>;
  ngt?: Maybe<Scalars['DateTime']>;
  ngte?: Maybe<Scalars['DateTime']>;
  nin?: Maybe<Array<Scalars['DateTime']>>;
  nlt?: Maybe<Scalars['DateTime']>;
  nlte?: Maybe<Scalars['DateTime']>;
}

export enum ConnectionTypes {
  Http = 'HTTP',
  Mqtt = 'MQTT',
  None = 'NONE'
}

export interface ConnectionTypesOperationFilterInput {
  eq?: Maybe<ConnectionTypes>;
  in?: Maybe<Array<ConnectionTypes>>;
  neq?: Maybe<ConnectionTypes>;
  nin?: Maybe<Array<ConnectionTypes>>;
}

export interface CreateDeviceInput {
  companyName: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  groupName?: Maybe<Scalars['String']>;
  ipv4: Scalars['String'];
  name: Scalars['String'];
  pluginName: Scalars['String'];
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
}

export interface CreateGroupInput {
  description?: Maybe<Scalars['String']>;
  name: Scalars['String'];
}

export interface Device {
  __typename?: 'Device';
  company: Company;
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  groups: Array<Group>;
  id: Scalars['String'];
  ip: IpAddress;
  lastModifiedAt: Scalars['DateTime'];
  lastModifiedBy: Scalars['String'];
  name: Scalars['String'];
  pluginName: Scalars['String'];
  pluginTypes: PluginTypes;
  primaryConnection: ConnectionTypes;
  secondaryConnection: ConnectionTypes;
  setDescription: BaseEntity;
  setName: BaseEntity;
  status?: Maybe<StatusResponseType>;
}

export interface DeviceSetDescriptionArgs {
  description: Scalars['String'];
}

export interface DeviceSetNameArgs {
  name: Scalars['String'];
}

export interface DeviceFilterInput {
  and?: Maybe<Array<DeviceFilterInput>>;
  company?: Maybe<CompanyFilterInput>;
  createdAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  createdBy?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  groups?: Maybe<ListFilterInputTypeOfGroupFilterInput>;
  id?: Maybe<StringOperationFilterInput>;
  ip?: Maybe<IpAddressFilterInput>;
  lastModifiedAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  lastModifiedBy?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  or?: Maybe<Array<DeviceFilterInput>>;
  pluginName?: Maybe<StringOperationFilterInput>;
  pluginTypes?: Maybe<PluginTypesOperationFilterInput>;
  primaryConnection?: Maybe<ConnectionTypesOperationFilterInput>;
  secondaryConnection?: Maybe<ConnectionTypesOperationFilterInput>;
}

export interface DeviceLightStateInput {
  deviceId: Scalars['String'];
  setLight: Scalars['Boolean'];
}

export interface DevicePayload {
  __typename?: 'DevicePayload';
  device?: Maybe<Device>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
}

export interface DeviceSortInput {
  company?: Maybe<CompanySortInput>;
  createdAt?: Maybe<SortEnumType>;
  createdBy?: Maybe<SortEnumType>;
  description?: Maybe<SortEnumType>;
  id?: Maybe<SortEnumType>;
  ip?: Maybe<IpAddressSortInput>;
  lastModifiedAt?: Maybe<SortEnumType>;
  lastModifiedBy?: Maybe<SortEnumType>;
  name?: Maybe<SortEnumType>;
  pluginName?: Maybe<SortEnumType>;
  pluginTypes?: Maybe<SortEnumType>;
  primaryConnection?: Maybe<SortEnumType>;
  secondaryConnection?: Maybe<SortEnumType>;
}

export interface DeviceStatePayload {
  __typename?: 'DeviceStatePayload';
  errors?: Maybe<Array<UserError>>;
  lightResponseType?: Maybe<LightResponseType>;
  message?: Maybe<Scalars['String']>;
}

export interface Group {
  __typename?: 'Group';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  devices: Array<Device>;
  id: Scalars['String'];
  lastModifiedAt: Scalars['DateTime'];
  lastModifiedBy: Scalars['String'];
  name: Scalars['String'];
  setDescription: BaseEntity;
  setName: BaseEntity;
}

export interface GroupSetDescriptionArgs {
  description: Scalars['String'];
}

export interface GroupSetNameArgs {
  name: Scalars['String'];
}

export interface GroupFilterInput {
  and?: Maybe<Array<GroupFilterInput>>;
  createdAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  createdBy?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  devices?: Maybe<ListFilterInputTypeOfDeviceFilterInput>;
  id?: Maybe<StringOperationFilterInput>;
  lastModifiedAt?: Maybe<ComparableDateTimeOffsetOperationFilterInput>;
  lastModifiedBy?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  or?: Maybe<Array<GroupFilterInput>>;
}

export interface GroupPayload {
  __typename?: 'GroupPayload';
  errors?: Maybe<Array<UserError>>;
  group?: Maybe<Group>;
  message?: Maybe<Scalars['String']>;
}

export interface GroupSortInput {
  createdAt?: Maybe<SortEnumType>;
  createdBy?: Maybe<SortEnumType>;
  description?: Maybe<SortEnumType>;
  id?: Maybe<SortEnumType>;
  lastModifiedAt?: Maybe<SortEnumType>;
  lastModifiedBy?: Maybe<SortEnumType>;
  name?: Maybe<SortEnumType>;
}

export interface IdentityPayload {
  __typename?: 'IdentityPayload';
  errors?: Maybe<Array<UserError>>;
  isAuthenticated: Scalars['Boolean'];
  message?: Maybe<Scalars['String']>;
  user?: Maybe<User>;
}

export interface InitPayload {
  __typename?: 'InitPayload';
  appConfig?: Maybe<AppConfig>;
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
}

export interface IpAddress {
  __typename?: 'IpAddress';
  ipv4: Scalars['String'];
}

export interface IpAddressFilterInput {
  and?: Maybe<Array<IpAddressFilterInput>>;
  ipv4?: Maybe<StringOperationFilterInput>;
  or?: Maybe<Array<IpAddressFilterInput>>;
}

export interface IpAddressSortInput {
  ipv4?: Maybe<SortEnumType>;
}

export interface LightResponseType {
  __typename?: 'LightResponseType';
  blue: Scalars['Int'];
  green: Scalars['Int'];
  ison: Scalars['Boolean'];
  mode: Scalars['String'];
  red: Scalars['Int'];
  white: Scalars['Int'];
}

export interface ListFilterInputTypeOfDeviceFilterInput {
  all?: Maybe<DeviceFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
  none?: Maybe<DeviceFilterInput>;
  some?: Maybe<DeviceFilterInput>;
}

export interface ListFilterInputTypeOfGroupFilterInput {
  all?: Maybe<GroupFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
  none?: Maybe<GroupFilterInput>;
  some?: Maybe<GroupFilterInput>;
}

export interface LoginInput {
  password: Scalars['String'];
  userName: Scalars['String'];
}

export interface NetworkDevice {
  __typename?: 'NetworkDevice';
  description?: Maybe<Scalars['String']>;
  hostname?: Maybe<Scalars['String']>;
  ipv4?: Maybe<Scalars['String']>;
  ipv6?: Maybe<Scalars['String']>;
  macAddress?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
}

export interface NetworkDeviceFilterInput {
  and?: Maybe<Array<NetworkDeviceFilterInput>>;
  description?: Maybe<StringOperationFilterInput>;
  hostname?: Maybe<StringOperationFilterInput>;
  ipv4?: Maybe<StringOperationFilterInput>;
  ipv6?: Maybe<StringOperationFilterInput>;
  macAddress?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  or?: Maybe<Array<NetworkDeviceFilterInput>>;
}

export interface NetworkDeviceSortInput {
  description?: Maybe<SortEnumType>;
  hostname?: Maybe<SortEnumType>;
  ipv4?: Maybe<SortEnumType>;
  ipv6?: Maybe<SortEnumType>;
  macAddress?: Maybe<SortEnumType>;
  name?: Maybe<SortEnumType>;
}

export interface PersonName {
  __typename?: 'PersonName';
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  middleName: Scalars['String'];
}

export enum PluginTypes {
  Base = 'BASE',
  Door = 'DOOR',
  Ht = 'HT',
  Light = 'LIGHT',
  Mock = 'MOCK',
  None = 'NONE',
  Rgb = 'RGB',
  Sensor = 'SENSOR'
}

export interface PluginTypesOperationFilterInput {
  eq?: Maybe<PluginTypes>;
  in?: Maybe<Array<PluginTypes>>;
  neq?: Maybe<PluginTypes>;
  nin?: Maybe<Array<PluginTypes>>;
}

export interface RegistrationInput {
  password: Scalars['String'];
  role: Scalars['String'];
  userName: Scalars['String'];
}

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export interface StatusResponseType {
  __typename?: 'StatusResponseType';
  lights: Array<LightResponseType>;
}

export interface StringOperationFilterInput {
  and?: Maybe<Array<StringOperationFilterInput>>;
  contains?: Maybe<Scalars['String']>;
  endsWith?: Maybe<Scalars['String']>;
  eq?: Maybe<Scalars['String']>;
  in?: Maybe<Array<Maybe<Scalars['String']>>>;
  ncontains?: Maybe<Scalars['String']>;
  nendsWith?: Maybe<Scalars['String']>;
  neq?: Maybe<Scalars['String']>;
  nin?: Maybe<Array<Maybe<Scalars['String']>>>;
  nstartsWith?: Maybe<Scalars['String']>;
  or?: Maybe<Array<StringOperationFilterInput>>;
  startsWith?: Maybe<Scalars['String']>;
}

export interface UpdateDeviceInput {
  description?: Maybe<Scalars['String']>;
  groupName?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  ipv4?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
  primaryConnection?: Maybe<ConnectionTypes>;
  secondaryConnection?: Maybe<ConnectionTypes>;
}

export interface UpdateGroupInput {
  description?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  name?: Maybe<Scalars['String']>;
}

export interface UpdateUserInput {
  email?: Maybe<Scalars['String']>;
  firstName?: Maybe<Scalars['String']>;
  lastName?: Maybe<Scalars['String']>;
  middleName?: Maybe<Scalars['String']>;
  newRole?: Maybe<Scalars['String']>;
  personInfo?: Maybe<Scalars['String']>;
  phoneNumber?: Maybe<Scalars['String']>;
  userId: Scalars['String'];
  userName?: Maybe<Scalars['String']>;
}

export interface User {
  __typename?: 'User';
  accessFailedCount: Scalars['Int'];
  concurrencyStamp?: Maybe<Scalars['String']>;
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  email?: Maybe<Scalars['String']>;
  emailConfirmed: Scalars['Boolean'];
  id?: Maybe<Scalars['String']>;
  isFirstLogin: Scalars['Boolean'];
  lastModifiedAt: Scalars['DateTime'];
  lastModifiedBy: Scalars['String'];
  lockoutEnabled: Scalars['Boolean'];
  lockoutEnd?: Maybe<Scalars['DateTime']>;
  normalizedEmail?: Maybe<Scalars['String']>;
  normalizedUserName?: Maybe<Scalars['String']>;
  passwordHash?: Maybe<Scalars['String']>;
  personInfo: Scalars['String'];
  personName: PersonName;
  phoneNumber?: Maybe<Scalars['String']>;
  phoneNumberConfirmed: Scalars['Boolean'];
  roles: Array<Scalars['String']>;
  securityStamp?: Maybe<Scalars['String']>;
  twoFactorEnabled: Scalars['Boolean'];
  userName?: Maybe<Scalars['String']>;
}

export interface UserError {
  __typename?: 'UserError';
  code: AppErrorCodes;
  message: Scalars['String'];
}

export interface UserPayload {
  __typename?: 'UserPayload';
  errors?: Maybe<Array<UserError>>;
  message?: Maybe<Scalars['String']>;
  user: User;
}
