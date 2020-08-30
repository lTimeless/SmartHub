import axios from 'axios';
import { getToken, logout } from '@/services/auth/authService';
import { useRouter } from 'vue-router';

export default axios.create({
  headers: {
    'Content-type': 'application/json'
  }
});

axios.interceptors.request.use(
  (config) => {
    console.log('interceptor');
    const token = getToken();
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

axios.interceptors.response.use(
  (response) => {
    if (!response.data.success) {
      console.log('interceptor response', response.data);
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
  (error) => Promise.reject(error),
);

