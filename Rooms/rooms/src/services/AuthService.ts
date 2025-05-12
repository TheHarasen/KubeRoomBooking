// authService.ts
import { setAccessToken, makeApi } from '../api/Api';
import { getConfig } from '../config';

interface LoginResponse {
  id: string;
  accessToken: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}

const getApi = () => {
  const { AUTHURL } = getConfig();
  return makeApi(AUTHURL);
};

export const refresh = async (): Promise<LoginResponse> => {
  const api = getApi();
  const res = await api.post<LoginResponse>('auth/refresh');
  setAccessToken(res.data.accessToken);
  return res.data;
};

export const login = async (email: string, password: string): Promise<LoginResponse> => {
const api = getApi();
  const res = await api.post<LoginResponse>('auth/login', { email, password });
  setAccessToken(res.data.accessToken);
  return res.data;
};

export const logout = async (): Promise<void> => {
const api = getApi();
  await api.post('auth/logout');
  setAccessToken(null);
};
