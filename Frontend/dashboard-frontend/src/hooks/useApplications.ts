import {
  useQuery,
  useMutation,
  useQueryClient,
  keepPreviousData,
} from "@tanstack/react-query";

import toast from "react-hot-toast";

import type { AppError } from "../api/api";

import {
  type CreateJobApplicationRequestDTO,
  type UpdateJobApplicationRequestDTO,
  type JobApplicationResponseDTO,
} from "../types/applications.types";

import {
  getJobApplicationList,
  createJobApplication,
  updateJobApplication,
} from "../services/applications.services";

export const jobApplicationKeys = {
  all: ["job-applications"] as const,
};

export const useJobApplicationList = () => {
  return useQuery<JobApplicationResponseDTO[], AppError>({
    queryKey: jobApplicationKeys.all,
    queryFn: () => getJobApplicationList(),
    placeholderData: keepPreviousData, //keep previous data while fetching new data
    staleTime: 1000 * 30, // fetch new data every 30 seconds
  });
};

export const useJobApplicationMutations = () => {
  const queryClient = useQueryClient();

  const invalidateList = () =>
    queryClient.invalidateQueries({ queryKey: jobApplicationKeys.all });

  const createMutation = useMutation<
    JobApplicationResponseDTO,
    AppError,
    CreateJobApplicationRequestDTO
  >({
    mutationFn: createJobApplication,
    onSuccess: () => {
      invalidateList();
      toast.success("Job application created successfully");
    },
    onError: (error: AppError) => {
      toast.error(error.message || "Failed to create job application");
    },
  });

  const updateMutation = useMutation<
    JobApplicationResponseDTO,
    AppError,
    UpdateJobApplicationRequestDTO
  >({
    mutationFn: updateJobApplication,
    onSuccess: () => {
      invalidateList();
      toast.success("Job application updated successfully");
    },
    onError: (error: AppError) => {
      toast.error(error.message || "Failed to update job application");
    },
  });

  return {
    createJobApplication: createMutation.mutate,
    isCreating: createMutation.isPending,
    createError: createMutation.error,

    updateJobApplication: updateMutation.mutate,
    isUpdating: updateMutation.isPending,
    updateError: updateMutation.error,
  };
};
