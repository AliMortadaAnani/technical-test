import { z } from "zod";

export const createJobApplicationFormSchema = z.object({
  name: z
    .string()
    .trim()
    .min(2, "Name is required and must be at least 2 characters")
    .max(100, "Maximum 100 characters"),
  email: z
    .email()
    .min(5, "Email is required and must be at least 5 characters")
    .max(100, "Maximum 100 characters"),
  jobTitle: z
    .string()
    .trim()
    .min(2, "Job title is required and must be at least 2 characters")
    .max(100, "Maximum 100 characters"),
});

export const updateJobApplicationFormSchema = z.object({
  id: z.number().int().positive("ID must be a positive integer"),
});
