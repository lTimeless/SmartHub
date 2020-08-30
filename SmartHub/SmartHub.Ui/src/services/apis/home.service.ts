import axios from 'axios';
import { Home, HomeCreateRequest, HomeUpdateRequest, ServerResponse } from '@/types/types';

const API_HOME_URL = 'api/Home';

export const getHome = (): Promise<ServerResponse<Home>> => axios.get<ServerResponse<Home>>(API_HOME_URL).then((res) => res.data);

export const postHome = (payload: HomeCreateRequest): Promise<ServerResponse<Home>> =>
  axios.post<ServerResponse<Home>>(API_HOME_URL, payload).then((res) => res.data);

export const putHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> =>
  axios.put<ServerResponse<Home>>(API_HOME_URL, payload).then((response) => response.data);

export const patchHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> =>
  axios.put<ServerResponse<Home>>(API_HOME_URL, payload).then((response) => response.data);
