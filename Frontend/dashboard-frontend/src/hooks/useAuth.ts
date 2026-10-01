import { useContext } from "react";
import { AuthContext } from "../contexts/authContext";

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be inside AuthProvider");
  return context;

  // so we can use it like this in any component:
  // const { isAuthenticated, login, logout } = useAuth();
}
