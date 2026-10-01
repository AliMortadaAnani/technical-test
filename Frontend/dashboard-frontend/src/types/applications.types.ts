import { z } from "zod";
import {
  createJobApplicationFormSchema,
  updateJobApplicationFormSchema,
} from "../schemas/applications.schemas";

export type CreateJobApplicationRequestDTO = z.infer<
  typeof createJobApplicationFormSchema
>;

export type UpdateJobApplicationRequestDTO = z.infer<
  typeof updateJobApplicationFormSchema
>;

export type JobApplicationResponseDTO = {
  id: number;
  name: string;
  email: string;
  jobTitle: string;
  status: string; // "New" | "In Progress" | "Done"
  //the asp.net server will return this enum as Json string as we configured it in the startup extension file
  createdAt: string;
};
