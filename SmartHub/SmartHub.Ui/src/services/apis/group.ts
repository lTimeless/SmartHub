import { Group, GroupCreateRequest, GroupUpdateRequest, ServerResponse } from '@/types/types';
import { api } from '@/router/axios/axios';

const API_GROUP_URL = 'api/Group';

export const getGroups = (): Promise<ServerResponse<Group>> =>
  api.get<ServerResponse<Group>>(API_GROUP_URL).then((res) => res.data);

export const postGroup = (payload: GroupCreateRequest): Promise<ServerResponse<Group>> =>
  api.post<ServerResponse<Group>>(API_GROUP_URL, payload).then((res) => res.data);

export const getByIdGroup = (payload: string): Promise<ServerResponse<Group>> =>
  api.get<ServerResponse<Group>>(`${API_GROUP_URL}/${payload}`).then((res) => res.data);

export const putByIdGroup = (payload: GroupUpdateRequest): Promise<ServerResponse<Group>> =>
  api.put<ServerResponse<Group>>(API_GROUP_URL, payload).then((res) => res.data);
