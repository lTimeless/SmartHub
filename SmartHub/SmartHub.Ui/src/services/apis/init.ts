import { api } from '@/router/axios/axios';
import { ServerResponse, AppConfigInitRequest, AppConfig } from '@/types/types';

const API_INIT_URL = 'api/Init';

export const postInit = (payload: AppConfigInitRequest): Promise<ServerResponse<AppConfig>> =>
  api.post<ServerResponse<AppConfig>>(API_INIT_URL, payload).then((res) => res.data);

export const checkHome = (): Promise<ServerResponse<boolean>> =>
  api.get<ServerResponse<boolean>>(`${API_INIT_URL}/checkHome`).then((res) => res.data);

export const checkUsers = (): Promise<ServerResponse<boolean>> =>
  api.get<ServerResponse<boolean>>(`${API_INIT_URL}/checkUsers`).then((res) => res.data);
