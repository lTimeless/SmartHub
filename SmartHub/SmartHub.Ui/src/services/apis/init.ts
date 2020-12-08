import { api } from '@/router/axios/axios';
import { Payload, AppConfigInitInput, AppConfig } from '@/types/types';

const API_INIT_URL = 'api/Init';

// export const postInit = (payload: AppConfigInitInput): Promise<Payload<AppConfig>> =>
//   api.post<Payload<AppConfig>>(API_INIT_URL, payload).then((res) => res.data);
//
// export const checkHome = (): Promise<Payload<boolean>> =>
//   api.get<Payload<boolean>>(`${API_INIT_URL}/checkHome`).then((res) => res.data);
//
// export const checkUsers = (): Promise<Payload<boolean>> =>
//   api.get<Payload<boolean>>(`${API_INIT_URL}/checkUsers`).then((res) => res.data);
