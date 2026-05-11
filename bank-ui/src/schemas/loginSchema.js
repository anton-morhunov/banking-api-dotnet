import { z } from "zod";

export const LoginSchema = z.object({
    
    email: z
        .string()
        .email("Your email or password is incorrect. Please try again."),
    
    passwordHash: z
        .string()
        .min(1, "Your email or password is incorrect. Please try again."),
})