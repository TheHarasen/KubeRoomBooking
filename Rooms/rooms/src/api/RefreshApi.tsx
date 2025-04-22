import axios, { AxiosError, AxiosInstance, AxiosRequestConfig } from 'axios';

let accessToken: string | null = null;

export const makeRefreshApi = (base: string): AxiosInstance => {

  const api = axios.create({
    baseURL: base,
    withCredentials: true, // Needed if your refresh token is in an httpOnly cookie
  })

  // Add access token to outgoing requests
  api.interceptors.request.use((config) => {
    if (accessToken && config.headers) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  });

  // Handle 401 errors and refresh access token
  api.interceptors.response.use(
    res => res,
    async (error: AxiosError) => {
      const originalRequest = error.config as AxiosRequestConfig & { _retry?: boolean };
  
      if (error.response?.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;
        try {
          const refreshResponse = await axios.post<{ accessToken: string }>(
            import.meta.env.VITE_AUTHURL + 'refresh',
            {},
            { withCredentials: true }
          );
  
          accessToken = refreshResponse.data.accessToken;
  
          if (originalRequest.headers) {
            originalRequest.headers.Authorization = `Bearer ${accessToken}`;
          }
  
          return api(originalRequest);
        } catch (refreshError) {
          console.error('Token refresh failed', refreshError);
          // Handle logout or redirect if needed
        }
      }
  
      return Promise.reject(error);
    }
  );
  
  return api;
};

export const setAccessToken = (token: string | null) => {
  accessToken = token;
};
