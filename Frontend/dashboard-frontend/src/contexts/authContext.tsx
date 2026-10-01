import { createContext, useState, type ReactNode } from "react";
import type { LoginRequestDTO } from "../types/auth.types";
import {
  login as loginService,
  logout as logoutService,
} from "../services/auth.services";
import { toast } from "react-hot-toast";

interface AuthContextType {
  isAuthenticated: boolean;
  login: (data: LoginRequestDTO) => boolean;
  logout: () => boolean;
}

export const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  //at mount the auth state is read from localStorage,
  //if empty => unAuthenticated, if there => authenticated

  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(() => {
    return localStorage.getItem("isAuthenticated") === "true";
  });

  function login(data: LoginRequestDTO) {
    const success = loginService(data);

    // 2. SAVE: If login succeeded, save to localStorage
    if (success) {
      localStorage.setItem("isAuthenticated", "true");
      setIsAuthenticated(true);
      toast.success("Login successful");
    }
    if (!success) {
      toast.error("Login failed. Please check your credentials.");
    }
    return success;
  }

  function logout() {
    const result = logoutService();

    // 3. REMOVE: Clear it from localStorage on logout
    localStorage.removeItem("isAuthenticated");
    setIsAuthenticated(false);

    return result;
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}
