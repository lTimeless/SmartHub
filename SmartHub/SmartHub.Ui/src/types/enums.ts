/*
  Enum for the application routes
*/
export enum Routes {
  // Identity
  Login = '/login',
  Registration = '/registration',
  Init = '/init',
  // Error
  NotFound = '/:patchMatch(.*)',
  NotAuthorized = '/notauth',
  // Me
  Me = '/me',
  // Home
  Layout = '/',
  Home = '/home',
  Groups = '/groups',
  Devices = '/devices',
  Users = '/users',
  Automations = '/automations',
  About = '/about', //  TODO Remove?
  Configuration = '/configuration', // TODO Configurations
  Plugins = '/plugins',
  Statistics = '/statistics',
  // Admin
  Activity = '/activity',
  Logs = '/logs',
  System = '/system',
  Health = '/health',
  Manager = '/manager' // TODO Remove??
}

/*
  Available Roles
*/
export enum Roles {
  None = 'None',
  Guest = 'Guest',
  User = 'User',
  Admin = 'Admin'
}

/*
  Available connections
*/
export enum ConnectionTypes {
  None = 0,
  Http = 1,
  Mqtt = 2
}

/*
  Available plugins
*/
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
