import { Group, CreateGroupInput, UpdateGroupInput, Payload } from '@/types/types';
import { api } from '@/router/axios/axios';

const API_GROUP_URL = 'api/Group';

// export const getGroups = (): Promise<Payload<Group>> =>
//   api.get<Payload<Group>>(API_GROUP_URL).then((res) => res.data);
//
// export const postGroup = (payload: CreateGroupInput): Promise<Payload<Group>> =>
//   api.post<Payload<Group>>(API_GROUP_URL, payload).then((res) => res.data);
//
// export const getByIdGroup = (payload: string): Promise<Payload<Group>> =>
//   api.get<Payload<Group>>(`${API_GROUP_URL}/${payload}`).then((res) => res.data);
//
// export const putByIdGroup = (payload: UpdateGroupInput): Promise<Payload<Group>> =>
//   api.put<Payload<Group>>(API_GROUP_URL, payload).then((res) => res.data);
