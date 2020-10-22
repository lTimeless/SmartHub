import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import { getToken, logout } from '@/services/auth/authService';
import { useRouter } from 'vue-router';

const config: AxiosRequestConfig = {
  headers: {
    'Content-type': 'application/json'
  }
};
const _axios = axios.create(config);

_axios.interceptors.request.use(
  (config) => {
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

_axios.interceptors.response.use(
  (response) => {
    // if (!response.data.success) {
    // TODO: trigger toast notification
    //   return Promise.reject(response.data.message);
    // }
    const router = useRouter();
    if (response.status === 401) {
      logout();
      router.push('/login');
    }
    return response;
  },
  (error) => Promise.reject(error)
);

export const api = _axios;
