import axiosInstance from '@/router/axios/axios';
import { Home, HomeCreateRequest, ServerResponse } from '@/types/types';

const API_INIT_URL = 'api/Init';

export const init = (payload: HomeCreateRequest): Promise<ServerResponse<Home>> =>
  axiosInstance.post<ServerResponse<Home>>(API_INIT_URL, payload).then((res) => res.data);
