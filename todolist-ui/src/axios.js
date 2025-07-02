import axios from "axios";
import { useAuthStore } from "./stores/auth";

const instance = axios.create({
    baseURL: "http://localhost:5000/api/",
    withCredentials: false,
    headers: {
        'Content-Type': 'application/json',
    }
});

instance.interceptors.request.use(config => {
    const auth = useAuthStore();
    const token = auth.accessToken;
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config; 
});

instance.interceptors.response.use(
  response => response,
  async error => {
    const originalRequest = error.config;
    const auth = useAuthStore();
    if (error.response && error.response.status === 401 && !originalRequest._retry && !originalRequest.url.includes('/Auth/refresh-token')) {
      originalRequest._retry = true;
      try {
        const userId = auth.user.userId;
        if (!userId) throw new Error ('Brak id zalogowanego użytkownika');
        const refreshToken = auth.refreshToken;
        if (!refreshToken) throw new Error('Brak refresh tokena');
        const response = await instance.post('/Auth/refresh-token', { userId, refreshToken});
        const { accessToken, refreshToken: newRefreshToken } = response.data;
        auth.saveTokens(accessToken, newRefreshToken);
        instance.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
        originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;
        return instance(originalRequest);
      } catch (refreshError) {
        console.error('Token refresh failed:', refreshError);
        auth.logout();
        window.location.href = '/';
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  }
);

export default instance;