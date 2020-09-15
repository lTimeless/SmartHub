import axios from 'axios';
import { getToken, logout } from '@/services/auth/authService';
import { useRouter } from 'vue-router';

export const Api = () => {
  const axiosInstance = axios.create({
    headers: {
      'Content-type': 'application/json'
    }
  });

  axiosInstance.interceptors.request.use(
    (config) => {
      const token = getToken();
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  axiosInstance.interceptors.response.use(
    (response) => {
      if (!response.data.success) {
        // TODO: trigger toast notification
        return Promise.reject(response.data.message);
      }
      const router = useRouter();
      if (response.status === 401) {
        logout();
        router.push('/login');
      }
      return response;
    },
    (error) => Promise.reject(error)
  );

  return axiosInstance;
};
