import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import Navbar from "./Navbar";
import Footer from "./Footer";

export default function PublicRoute() {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to="/dashboard" replace />;
  }

  return (
    <>
      <div className="min-h-screen flex flex-col bg-gray-300">
        <Navbar
          linkLabels={["Login", "About"]}
          linkTo={["/login", "/about"]}
          withLogoutButton={false}
        />
        <main className="flex-1 max-w-7xl w-full mx-auto p-6">
          <Outlet />
        </main>
        <Footer />
      </div>
    </>
  );
}
