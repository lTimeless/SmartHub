import { Group, GroupCreateRequest, ServerResponse } from '@/types/types';
import { Api } from '@/router/axios/axios';

const API_GROUP_URL = 'api/Group';

export const getGroups = (): Promise<ServerResponse<Group>> =>
  Api()
    .get<ServerResponse<Group>>(API_GROUP_URL)
    .then((res) => res.data);

export const postGroup = (payload: GroupCreateRequest): Promise<ServerResponse<Group>> =>
  Api()
    .post<ServerResponse<Group>>(API_GROUP_URL, payload)
    .then((res) => res.data);

// export const postHome = (payload: HomeCreateRequest): Promise<ServerResponse<Home>> =>
//     Api()
//         .post<ServerResponse<Home>>(API_HOME_URL, payload)
//         .then((res) => res.data);
