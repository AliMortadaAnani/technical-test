import type { LoginRequestDTO } from "../types/auth.types.ts";

export function login(data: LoginRequestDTO): boolean {
  return data.username === "admin" && data.password === "1234";
}
export function logout(): boolean {
  return false;
}
