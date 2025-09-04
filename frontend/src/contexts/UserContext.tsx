import { createContext, useState, useContext, type ReactNode } from 'react';
import type { Account } from '../types';

type User = {
  id: number;
  username: string;
  email: string;
  phone: string;
  accounts: Account[];
}

type UserContextType = {
  user: User | undefined;
  setUser: (user: User | undefined) => void;
};
const UserContext = createContext<UserContextType | undefined>(undefined);

export function UserProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | undefined>(undefined);

  return (
    <UserContext.Provider value={{ user, setUser }}>
      {children}
    </UserContext.Provider>
  );
}

export function useUser() {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error('useUser must be used within a UserProvider');
  }
  return context;
}