import axios from 'axios';
import { getAuthentication, isAuthenticated } from '@/services/auth/authService';

axios.interceptors.request.use(
  (config) => {
    if (isAuthenticated()) {
      config.headers = getAuthentication().headers;
    }
    return config;
  },
  (error) => Promise.reject(error)
);
