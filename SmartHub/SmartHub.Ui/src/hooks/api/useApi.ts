import { reactive, toRefs, UnwrapRef, Ref } from 'vue';
import { api } from '@/router/axios/axios';
import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ServerResponse } from '@/types/types';

interface ApiState<T> {
  data: ServerResponse<T>;
  error: null;
}

export type ApiRequest = () => Promise<void>;

export type ApiResponse<T> = {
  data: Ref<UnwrapRef<ServerResponse<T>>>;
  error: Ref<null>;
  request: ApiRequest;
};

export function useApi<T>(requestConfig: AxiosRequestConfig): ApiResponse<T> {
  const state = reactive<ApiState<T>>({
    data: {} as ServerResponse<T>,
    error: null
  });

  const request = async (): Promise<void> =>
    await api
      .request<T>(requestConfig)
      .then((data: AxiosResponse<T>) => {
        state.data = data.data as UnwrapRef<ServerResponse<T>>;
      })
      .catch((err) => (state.error = err));
  return {
    request,
    ...toRefs(state)
  };
}
