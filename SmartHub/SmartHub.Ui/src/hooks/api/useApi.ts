import { reactive, toRefs, UnwrapRef, Ref } from 'vue';
import { api } from '@/router/axios/axios';
import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { Payload } from '@/types/types';

interface ApiState<T> {
  data: Payload<T>;
  error: null;
}

export type ApiRequest = () => Promise<void>;

export type ApiResponse<T> = {
  data: Ref<UnwrapRef<Payload<T>>>;
  error: Ref<null>;
  request: ApiRequest;
};

export function useApi<T>(requestConfig: AxiosRequestConfig): ApiResponse<T> {
  const state = reactive<ApiState<T>>({
    data: {} as Payload<T>,
    error: null
  });

  const request = async (): Promise<void> =>
    await api
      .request<T>(requestConfig)
      .then((data: AxiosResponse<T>) => {
        state.data = data.data as UnwrapRef<Payload<T>>;
      })
      .catch((err) => (state.error = err));
  return {
    request,
    ...toRefs(state)
  };
}
