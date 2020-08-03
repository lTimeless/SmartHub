import { getAuthentication, isAuthenticated } from '@/services/auth/authService';
import axiosInstance from '@/router/axios/axios';

axiosInstance.interceptors.request.use(
  (config) => {
    if (isAuthenticated()) {
      config.headers = getAuthentication().headers;
    }
    return config;
  },
  (error) => Promise.reject(error)
);
