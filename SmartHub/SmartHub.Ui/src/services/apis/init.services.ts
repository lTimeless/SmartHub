import { api } from '@/router/axios/axios';
import { ServerResponse } from '@/types/types';

const API_INIT_URL = 'api/Init';

export const checkHome = (): Promise<ServerResponse<boolean>> =>
  api()
    .get<ServerResponse<boolean>>(`${API_INIT_URL}/checkHome`)
    .then((res) => res.data);

export const checkUsers = (): Promise<ServerResponse<boolean>> =>
    api()
      .get<ServerResponse<boolean>>(`${API_INIT_URL}/checkUsers`)
      .then((res) => res.data);