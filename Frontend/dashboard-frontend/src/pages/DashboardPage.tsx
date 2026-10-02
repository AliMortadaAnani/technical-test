import {
  useJobApplicationList,
  useJobApplicationMutations,
} from "../hooks/useApplications";
import {
  type JobApplicationResponseDTO,
  type UpdateJobApplicationRequestDTO,
} from "../types/applications.types";

export default function JobApplicationTable() {
  const {
    data: applications,
    isLoading,
    isError,
    error,
  } = useJobApplicationList();

  const { updateJobApplication, isUpdating } = useJobApplicationMutations();

  const handleUpdate = (payload: UpdateJobApplicationRequestDTO) => {
    updateJobApplication(payload);
  };

  // Helper to color-code status badges
  const getStatusBadge = (status: string) => {
    switch (status) {
      case "New":
        return "bg-blue-50 text-blue-700 border-blue-200";
      case "InProgress":
        return "bg-amber-50 text-amber-700 border-orange-200";
      case "Done":
        return "bg-green-50 text-green-700 border-green-200";
      default:
        return "bg-gray-50 text-gray-700 border-gray-200";
    }
  };

  if (isLoading) {
    return (
      <div className="p-8 text-center text-sm text-gray-500">
        Loading applications...
      </div>
    );
  }

  if (isError) {
    return (
      <div className="p-8 text-center text-sm text-red-800">
        Error loading applications: {error?.message || "Something went wrong"}
      </div>
    );
  }

  return (
    <div className="w-full bg-white rounded-xl border border-gray-200 shadow-sm overflow-hidden">
      {/* Responsive table container */}
      <div className="overflow-x-auto">
        <table className="w-full text-left text-sm text-gray-600">
          {/* Table Header */}
          <thead className="bg-gray-50 text-xs uppercase text-gray-700 font-semibold border-b border-gray-200">
            <tr>
              <th className="px-6 py-4">Applicant</th>
              <th className="px-6 py-4">Job Title</th>
              <th className="px-6 py-4">Status</th>
              <th className="px-6 py-4">Date</th>
              <th className="px-6 py-4 text-right">Action</th>
            </tr>
          </thead>

          {/* Table Body */}
          <tbody className="divide-y divide-gray-200">
            {applications && applications.length > 0 ? (
              applications.map((app: JobApplicationResponseDTO) => (
                <tr
                  key={app.id}
                  className="hover:bg-gray-50 transition-colors duration-150"
                >
                  {/* Name & Email */}
                  <td className="px-6 py-4">
                    <p className="font-medium text-gray-900">{app.name}</p>
                    <p className="text-xs text-gray-500">{app.email}</p>
                  </td>

                  {/* Job Title */}
                  <td className="px-6 py-4 font-medium text-gray-800">
                    {app.jobTitle}
                  </td>

                  {/* Status Badge */}
                  <td className="px-6 py-4">
                    <span
                      className={`inline-flex items-center px-2.5 py-1 rounded-full text-xs font-medium border ${getStatusBadge(
                        app.status,
                      )}`}
                    >
                      {app.status}
                    </span>
                  </td>

                  {/* Date Created */}
                  <td className="px-6 py-4 text-gray-500 text-xs">
                    {new Date(app.createdAt).toLocaleDateString()}
                  </td>

                  {/* Action Button */}
                  <td className="px-6 py-4 text-right">
                    {app.status === "New" && (
                      <button
                        type="button"
                        disabled={isUpdating}
                        onClick={() => handleUpdate({ id: app.id })}
                        className="px-3 py-1.5 bg-amber-600 hover:bg-amber-800 disabled:bg-amber-300 text-taupe-100 border border-amber-200   text-xs font-medium rounded-lg transition-colors duration-150"
                      >
                        Start Progress
                      </button>
                    )}

                    {app.status === "InProgress" && (
                      <button
                        type="button"
                        disabled={isUpdating}
                        onClick={() => handleUpdate({ id: app.id })}
                        className="px-3 py-1.5 bg-green-600 hover:bg-green-700 disabled:bg-green-300 text-white text-xs font-medium rounded-lg transition-colors duration-150"
                      >
                        Mark Done
                      </button>
                    )}

                    {app.status === "Done" && (
                      <span className="text-xs text-gray-400 font-medium">
                        Completed
                      </span>
                    )}
                  </td>
                </tr>
              ))
            ) : (
              // if there are no applications in the list
              <tr>
                <td colSpan={5} className="px-6 py-8 text-center text-gray-400">
                  No job applications found.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
