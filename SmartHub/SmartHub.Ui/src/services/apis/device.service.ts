import { Device, DeviceCreateRequest, DeviceUpdateRequest, ServerResponse } from '@/types/types';
import { api } from '@/router/axios/axios';

const API_DEVICE_URL = 'api/Device';

export const postDevice = (payload: DeviceCreateRequest): Promise<ServerResponse<Device>> =>
  api.post<ServerResponse<Device>>(API_DEVICE_URL, payload).then((res) => res.data);

export const getByIdDevice = (payload: string): Promise<ServerResponse<Device>> =>
  api.get<ServerResponse<Device>>(`${API_DEVICE_URL}/${payload}`).then((res) => res.data);

export const putByIdDevice = (payload: DeviceUpdateRequest): Promise<ServerResponse<Device>> =>
  api.put<ServerResponse<Device>>(API_DEVICE_URL, payload).then((res) => res.data);
