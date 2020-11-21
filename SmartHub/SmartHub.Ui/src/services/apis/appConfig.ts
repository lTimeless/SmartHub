import { api } from '@/router/axios/axios';
import { ServerResponse, AppConfig } from '@/types/types';

const API_INIT_URL = 'api/Init';

export const getAppConfig = (): Promise<ServerResponse<AppConfig>> =>
  api.get<ServerResponse<AppConfig>>(API_INIT_URL).then((res) => res.data);
