import { Device, CreateDeviceInput, UpdateDeviceInput, Payload } from '@/types/types';
import { api } from '@/router/axios/axios';

const API_DEVICE_URL = 'api/Device';

// export const postDevice = (payload: CreateDeviceInput): Promise<Payload<Device>> =>
//   api.post<Payload<Device>>(API_DEVICE_URL, payload).then((res) => res.data);
//
// export const getByIdDevice = (payload: string): Promise<Payload<Device>> =>
//   api.get<Payload<Device>>(`${API_DEVICE_URL}/${payload}`).then((res) => res.data);
//
// export const putByIdDevice = (payload: UpdateDeviceInput): Promise<Payload<Device>> =>
//   api.put<Payload<Device>>(API_DEVICE_URL, payload).then((res) => res.data);
