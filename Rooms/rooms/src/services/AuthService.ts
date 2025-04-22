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

export interface RegisterRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  passwordRepeat: string;
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

export const register = async (request: RegisterRequest): Promise<{ message: string, success: boolean}> => {
  if (request.password != request.passwordRepeat)
    return { message: "Not the same password", success: false };

  const api = makeApi(import.meta.env.VITE_AUTHURL);
  if(request.role == "student")
    await api.post("students", {  email: request.email,
      firstName: request.firstName,
      lastName: request.lastName,
      password: request.password,});
  else
    await api.post("teachers", {  email: request.email,
      firstName: request.firstName,
      lastName: request.lastName,
      password: request.password,});

  return { message: "Account made", success: true };
};

export const update = async (request: RegisterRequest): Promise<{ message: string, success: boolean}> => {
 
  return { message: "Account updated", success: true };
};
