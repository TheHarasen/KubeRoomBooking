// authService.ts
import { setAccessToken, makeApi } from '../api/Api';

interface LoginResponse {
  id: string;
  accessToken: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}

export const refresh = async (): Promise<LoginResponse> => {
  const api = makeApi(import.meta.env.VITE_AUTHURL);
  const res = await api.post<LoginResponse>('auth/refresh');
  setAccessToken(res.data.accessToken);
  return res.data;
};

export const login = async (email: string, password: string): Promise<LoginResponse> => {
  const api = makeApi(import.meta.env.VITE_AUTHURL);
  const res = await api.post<LoginResponse>('auth/login', { email, password });
  setAccessToken(res.data.accessToken);
  return res.data;
};

export const logout = async (): Promise<void> => {
  const api = makeApi(import.meta.env.VITE_AUTHURL);
  await api.post('auth/logout');
  setAccessToken(null);
};
