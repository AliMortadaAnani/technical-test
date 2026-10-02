import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import Navbar from "./Navbar";
import Footer from "./Footer";
export default function ProtectedRoute() {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  return (
    <>
      <div className="min-h-screen flex flex-col bg-gray-300">
        <Navbar
          link1Label="Dashboard"
          link1To="/dashboard"
          link2Label="Create Job Application"
          link2To="/new-job-application"
          withLogoutButton={true}
        />
        <main className="flex-1 max-w-7xl w-full mx-auto p-6">
          <Outlet />
        </main>
        <Footer />
      </div>
    </>
  );
}
