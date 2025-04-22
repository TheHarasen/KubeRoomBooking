import axios, { AxiosError, AxiosInstance, AxiosRequestConfig } from 'axios';

let accessToken: string | null = null;

export const makeApi = (base: string): AxiosInstance => {

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
  
  return api;
};

export const setAccessToken = (token: string | null) => {
  accessToken = token;
};
