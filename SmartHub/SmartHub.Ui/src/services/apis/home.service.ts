import { Home, HomeCreateRequest, HomeUpdateRequest, ServerResponse } from '@/types/types';
import { Api } from '@/router/axios/axios';

const API_HOME_URL = 'api/Home';

export const getHome = (): Promise<ServerResponse<Home>> =>
  Api()
    .get<ServerResponse<Home>>(API_HOME_URL)
    .then((res) => res.data);

export const postHome = (payload: HomeCreateRequest): Promise<ServerResponse<Home>> =>
  Api()
    .post<ServerResponse<Home>>(API_HOME_URL, payload)
    .then((res) => res.data);

export const putHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> =>
  Api()
    .put<ServerResponse<Home>>(API_HOME_URL, payload)
    .then((response) => response.data);

export const patchHome = (payload: HomeUpdateRequest): Promise<ServerResponse<Home>> =>
  Api()
    .put<ServerResponse<Home>>(API_HOME_URL, payload)
    .then((response) => response.data);
