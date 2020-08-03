import axios from 'axios';

const axiosInstance = axios.create({
  timeout: 10000,
  params: {} // do not remove this, its added to add params later in the config
});

export default axiosInstance;
