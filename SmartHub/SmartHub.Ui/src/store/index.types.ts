import { AppConfig } from '@/types/types';

export interface RootState {
  appModule: AppState;
}

/*
  State for the home data
*/
export interface AppState {
  appConfig?: AppConfig;
  userDropDownOpen: boolean;
  mobileSidebarOpen: boolean;
  miniSidebarOpen: boolean;
}
