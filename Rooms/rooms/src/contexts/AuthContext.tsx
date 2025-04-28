import { createContext, useContext, useState, ReactNode, useEffect } from 'react';
import { login as loginService, logout as logoutService, refresh as refreshService } from '../services/AuthService';
import { User } from '../types/User';

interface AuthContextType {
  loggedIn: User | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  refresh: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [loggedIn, setLoggedIn] = useState<User | null>(null);

  useEffect(() => {
    refresh();
  }, []);

  const login = async (email: string, password: string) => {
    const res = await loginService(email, password);
    if(res != null)
      setLoggedIn({ id: res.id, email: res.email, firstName: res.firstName, lastName: res.lastName, role: res.role});
  };

  const logout = async () => {
    await logoutService();
    setLoggedIn(null);
  };

  const refresh = async () => {
    const res = await refreshService();
    if(res != null)
      setLoggedIn({ id: res.id, email: res.email, firstName: res.firstName, lastName: res.lastName, role: res.role});
  }

  return (
    <AuthContext.Provider value={{ loggedIn, login, logout, refresh }}>
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
