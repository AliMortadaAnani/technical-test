import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";

// route (and layout) guards
import ProtectedRoute from "./components/ProtectedRoute";
import PublicRoute from "./components/PublicRoute";

//page imports
import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import CreateJobApplicationPage from "./pages/CreateJobApplicationPage";
import AboutPage from "./pages/AboutPage";

import { AuthProvider } from "./contexts/authContext";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: true,
      retry: false,
    },
  },
});

export function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            {/* Public Routes */}
            <Route element={<PublicRoute />}>
              <Route path="/login" element={<LoginPage />} />
              <Route path="/about" element={<AboutPage />} />
            </Route>

            {/* Protected Routes (mock auth system) */}
            <Route element={<ProtectedRoute />}>
              <Route path="/" element={<Navigate to="/dashboard" replace />} />
              <Route path="/dashboard" element={<DashboardPage />} />
              <Route
                path="/new-job-application"
                element={<CreateJobApplicationPage />}
              />
            </Route>

            {/* Catch-all redirect */}
            <Route path="*" element={<Navigate to="/dashboard" replace />} />
          </Routes>
        </BrowserRouter>
        <Toaster
          position="top-right"
          toastOptions={{
            className:
              "text-sm font-medium rounded-lg shadow-md border border-gray-100 px-3 py-2",
          }}
        />
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
