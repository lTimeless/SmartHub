import { AppConfig, Device, Group } from '@/types/types';

export interface RootState {
  appModule: AppState;
}

/*
  State for the home data
*/
export interface AppState {
  groups?: Group[];
  devices?: Device[];
  appConfig?: AppConfig;
  userDropDownOpen: boolean;
  notificationDropdownOpen: boolean;
}
