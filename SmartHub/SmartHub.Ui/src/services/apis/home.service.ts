import axiosInstance from '@/router/axios/axios';
import { Home, HomeUpdateRequest, ServerResponse } from '@/types/types';
import { getAuthentication } from '@/services/auth/authService';

const API_HOME_URL = 'api/Home';

export const getHome = (): Promise<ServerResponse<Home>> => axiosInstance.get<ServerResponse<Home>>(API_HOME_URL).then((res) => res.data);

export const putHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> => {
  axiosInstance.defaults.headers = getAuthentication().headers;
  return axiosInstance.put<ServerResponse<Home>>('api/home', payload).then((response) => response.data);
};

export const patchHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> => {
  axiosInstance.defaults.headers = getAuthentication().headers;
  return axiosInstance.put<ServerResponse<Home>>('api/home', payload).then((response) => response.data);
};
