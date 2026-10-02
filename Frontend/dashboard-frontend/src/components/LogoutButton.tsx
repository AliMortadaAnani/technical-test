import { useAuth } from "../hooks/useAuth";

export default function LogoutButton() {
  const { logout } = useAuth();

  const handleLogout = async () => {
    logout();
  };

  return (
    <button
      type="button"
      onClick={handleLogout}
      className={`px-3 py-1.5 text-sm font-medium text-red-600 bg-red-50 hover:bg-red-100 rounded-lg transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-red-200 focus:ring-offset-2`}
    >
      Logout
    </button>
  );
}
