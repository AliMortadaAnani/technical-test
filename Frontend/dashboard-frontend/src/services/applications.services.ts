import { api } from "../api/api";
import {
  type CreateJobApplicationRequestDTO,
  type UpdateJobApplicationRequestDTO,
  type JobApplicationResponseDTO,
} from "../types/applications.types";

export const getJobApplicationList = async (): Promise<
  JobApplicationResponseDTO[]
> => {
  const res = await api.get<JobApplicationResponseDTO[]>("/job-applications");
  return res.data;
};

export const createJobApplication = async (
  dto: CreateJobApplicationRequestDTO,
): Promise<JobApplicationResponseDTO> => {
  const res = await api.post<JobApplicationResponseDTO>(
    "/job-application",
    dto,
  );
  return res.data;
};

export const updateJobApplication = async (
  dto: UpdateJobApplicationRequestDTO,
): Promise<JobApplicationResponseDTO> => {
  const res = await api.put<JobApplicationResponseDTO>("/job-application", dto);
  return res.data;
};
