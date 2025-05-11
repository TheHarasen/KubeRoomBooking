import { makeApi } from "../api/Api";

export interface RegisterRequest {
  id?: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  passwordRepeat: string;
  role: string;
}

export const register = async (request: RegisterRequest): Promise<{ message: string, success: boolean }> => {
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

export const updateSelf = async (request: RegisterRequest): Promise<{ message: string, success: boolean}> => {
  const api = makeApi(import.meta.env.VITE_AUTHURL);
  if(request.role == "student")
    await api.put("students", {
      id: request.id,
      firstName: request.firstName,
      lastName: request.lastName,
      email: request.email,
      passwordOld: request.password,
      passwordNew: request.passwordRepeat
    });
  else
    await api.put("teachers", {
      id: request.id,
      firstName: request.firstName,
      lastName: request.lastName,
      email: request.email,
      passwordOld: request.password,
      passwordNew: request.passwordRepeat
    });
  return { message: "Account updated", success: true };
};