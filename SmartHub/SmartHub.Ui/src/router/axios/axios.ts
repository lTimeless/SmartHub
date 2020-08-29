import axios from 'axios';
import { getAuthentication, isAuthenticated } from '@/services/auth/authService';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:8080',
  timeout: 8000,
  params: {} // do not remove this, its added to add params later in the config
});

axiosInstance.interceptors.request.use(
  (config) => {
    if (isAuthenticated()) {
      config.headers = getAuthentication().headers;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default axiosInstance;
