import { Device, DeviceCreateRequest, ServerResponse } from '@/types/types';
import { Api } from '@/router/axios/axios';

const API_DEVICE_URL = 'api/Device';

export const postDevice = (payload: DeviceCreateRequest): Promise<ServerResponse<Device>> =>
  Api()
    .post<ServerResponse<Device>>(API_DEVICE_URL, payload)
    .then((res) => res.data);

// export const putByIdGroup = (payload: DeviceUpdateRequest): Promise<ServerResponse<Device>> =>
//   Api()
//     .put<ServerResponse<Device>>(API_GROUP_URL, payload)
//     .then((res) => res.data);
