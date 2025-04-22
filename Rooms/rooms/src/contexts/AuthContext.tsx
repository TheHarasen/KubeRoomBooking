// AuthContext.tsx
import { createContext, useContext, useState, ReactNode, useEffect } from 'react';
import { login as loginService, logout as logoutService, refresh as refreshService } from '../services/AuthService';

interface User {
  email: string;
  firstName: string;
  lastName: string;
}

interface AuthContextType {
  user: User | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  refresh: () => Promise<void>;
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
      setUser({email: res.email, firstName: res.firstName, lastName: res.lastName});
  };

  const logout = async () => {
    await logoutService();
    setUser(null);
  };

  const refresh = async () => {
    const res = await refreshService();
    if(res != null)
      setUser({email: res.email, firstName: res.firstName, lastName: res.lastName});
  }

  return (
    <AuthContext.Provider value={{ user, login, logout, refresh }}>
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
