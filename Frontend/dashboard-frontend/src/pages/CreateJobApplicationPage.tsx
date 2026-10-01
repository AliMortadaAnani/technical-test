import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useJobApplicationMutations } from "../hooks/useApplications";
import { createJobApplicationFormSchema } from "../schemas/applications.schemas";
import { type CreateJobApplicationRequestDTO } from "../types/applications.types";

export default function CreateJobApplicationForm() {
  // 1. Consume the creation mutation and loading state
  const { createJobApplication, isCreating } = useJobApplicationMutations();

  // 2. Initialize React Hook Form with Zod validation
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateJobApplicationRequestDTO>({
    resolver: zodResolver(createJobApplicationFormSchema),
    defaultValues: {
      name: "",
      email: "",
      jobTitle: "",
    },
  });

  // 3. Handle form submission
  const onSubmit = (data: CreateJobApplicationRequestDTO) => {
    createJobApplication(data, {
      onSuccess: () => {
        // Clear the form fields after successful creation
        reset();
      },
    });
  };

  return (
    <div className="w-full max-w-lg m-auto mb-3 mt-3 bg-white rounded-xl shadow-sm border border-gray-200 p-6">
      <div className="mb-6">
        <h2 className="text-xl font-bold text-gray-900">New Job Application</h2>
        <p className="text-sm text-gray-500 mt-1">
          Fill out the details below to submit a new applicant.
        </p>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        {/* Full Name Field */}
        <div>
          <label
            htmlFor="name"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Full Name
          </label>
          <input
            id="name"
            type="text"
            placeholder="e.g., Jane Doe"
            {...register("name")}
            className={`w-full px-3 py-2 border rounded-lg text-sm transition-colors focus:outline-none focus:ring-2 ${
              errors.name
                ? "border-red-500 focus:ring-red-200"
                : "border-gray-300 focus:ring-blue-100 focus:border-blue-600"
            }`}
          />
          {errors.name && (
            <p className="text-xs text-red-500 mt-1">{errors.name.message}</p>
          )}
        </div>

        {/* Email Address Field */}
        <div>
          <label
            htmlFor="email"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Email Address
          </label>
          <input
            id="email"
            type="email"
            placeholder="e.g., jane.doe@example.com"
            {...register("email")}
            className={`w-full px-3 py-2 border rounded-lg text-sm transition-colors focus:outline-none focus:ring-2 ${
              errors.email
                ? "border-red-500 focus:ring-red-200"
                : "border-gray-300 focus:ring-blue-100 focus:border-blue-600"
            }`}
          />
          {errors.email && (
            <p className="text-xs text-red-500 mt-1">{errors.email.message}</p>
          )}
        </div>

        {/* Job Title Field */}
        <div>
          <label
            htmlFor="jobTitle"
            className="block text-sm font-medium text-gray-700 mb-1"
          >
            Job Title
          </label>
          <input
            id="jobTitle"
            type="text"
            placeholder="e.g., Frontend Developer"
            {...register("jobTitle")}
            className={`w-full px-3 py-2 border rounded-lg text-sm transition-colors focus:outline-none focus:ring-2 ${
              errors.jobTitle
                ? "border-red-500 focus:ring-red-200"
                : "border-gray-300 focus:ring-blue-100 focus:border-blue-600"
            }`}
          />
          {errors.jobTitle && (
            <p className="text-xs text-red-500 mt-1">
              {errors.jobTitle.message}
            </p>
          )}
        </div>

        {/* Submit Button */}
        <button
          type="submit"
          disabled={isCreating}
          className="w-full mt-2 py-2.5 px-4 bg-blue-600 hover:bg-blue-700 disabled:bg-blue-300 text-white font-medium rounded-lg text-sm transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
        >
          {isCreating ? "Creating Application..." : "Create Application"}
        </button>
      </form>
    </div>
  );
}
