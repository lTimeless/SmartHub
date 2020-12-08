import { api } from '@/router/axios/axios';
import { Payload, AppConfig } from '@/types/types';

const API_INIT_URL = 'api/Init';

export const getAppConfig = (): Promise<Payload<AppConfig>> =>
  api.get<Payload<AppConfig>>(API_INIT_URL).then((res) => res.data);
