import axios from "axios";

//here we are mapping the ProblemDetails type from the ASP.NET so we can deal with it properly
export type ProblemDetails = {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
  errors?: Record<string, string[]>; // this array is returned from ASP.NET or FluentValidation
  traceId?: string;
};

// here e are defining a custom error type that will cover all our needs in UI
export type AppError = Error & {
  title?: string; // title from ProblemDetails
  status?: number;
  errors?: Record<string, string[]>; //from ASP.NET or FluentValidation
};

export const api = axios.create({
  baseURL:
    import.meta.env.VITE_API_BASE_URL || "http://localhost:7000/api/Dashboard",
  headers: {
    "Content-Type": "application/json", //body payload is in JSON
  },
});

api.interceptors.response.use(
  (response) => response,

  (error) => {
    // no response from the server => a network error or server is offline
    //caused by the server being offline, CORS,...
    if (!error.response) {
      const message = "Network error: Server is offline or unreachable.";

      const offlineError: AppError = new Error(message);
      offlineError.title = "NETWORK_ERROR";
      offlineError.status = 0;
      offlineError.errors = {};

      return Promise.reject(offlineError);
    }

    // if server returns an error response, we map it to our AppError type here before passing it to our components

    const problem: ProblemDetails = error.response.data || {};

    const fieldErrors = problem.errors
      ? Object.values(problem.errors).flat().join(" ")
      : ""; //from ASP.NET or FluentValidation, we can have multiple errors for a single field, so we flatten them into a single string
    //In normal cases we should not obtain this error message expect if our handling was bypassed
    // zod and typescript should handle this before sending the request to the backend

    const message =
      fieldErrors ||
      problem.detail ||
      problem.title ||
      "An unexpected error occurred.";

    const uiError: AppError = new Error(message);
    uiError.title =
      problem.title ?? error.response.statusText ?? "UNKNOWN_ERROR";
    uiError.status = problem.status ?? error.response.status ?? 500;
    uiError.errors = problem.errors ?? {};

    return Promise.reject(uiError);
  },
);
