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
      <Navbar
        link1Label="Dashboard"
        link1To="/dashboard"
        link2Label="Create Job Application"
        link2To="/new-job-application"
        withLogoutButton={true}
      />
      <Outlet />
      <Footer />
    </>
  );
}
