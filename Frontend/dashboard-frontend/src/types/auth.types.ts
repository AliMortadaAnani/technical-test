import { z } from "zod";
import { loginFormSchema } from "../schemas/auth.schemas";

export type LoginRequestDTO = z.infer<typeof loginFormSchema>;
