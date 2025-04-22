// AuthContext.tsx
import { createContext, useContext, useState, ReactNode, useEffect } from 'react';
import { update as updateService, register as registerService, login as loginService, logout as logoutService, refresh as refreshService, RegisterRequest } from '../services/AuthService';

interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string
}

interface AuthContextType {
  user: User | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  refresh: () => Promise<void>;
  register: (request: RegisterRequest) => Promise<{ message: string, success: boolean }>;
  update: (request: RegisterRequest) => Promise<{ message: string, success: boolean }>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    refresh();
  }, []);

  const login = async (email: string, password: string) => {
    const res = await loginService(email, password);
    if(res != null)
      setUser({ id: res.id, email: res.email, firstName: res.firstName, lastName: res.lastName, role: res.role});
  };

  const logout = async () => {
    await logoutService();
    setUser(null);
  };

  const refresh = async () => {
    const res = await refreshService();
    if(res != null)
      setUser({ id: res.id, email: res.email, firstName: res.firstName, lastName: res.lastName, role: res.role});
  }

  const register = async (request: RegisterRequest) => {
    return await registerService(request);
  }

  const update = async (request: RegisterRequest) => {
    return await updateService(request);
  }

  return (
    <AuthContext.Provider value={{ user, login, logout, refresh, register, update }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
};
