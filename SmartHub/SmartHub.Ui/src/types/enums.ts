export enum Roles {
  None = 'None',
  Guest = 'Guest',
  User = 'User',
  Admin = 'Admin'
}

export enum ConnectionTypes {
  None = 0,
  Http = 1,
  Mqtt = 2
}

export enum PluginTypes {
  None = 0,
  Base = 1,
  Mock = 2,
  Door = 4,
  Light = 8,
  Ht = 16, // humidity and temperature sensor
  Sensor = 32, //  default if it is not defined
  Rgb = 64 // red green blue
}
