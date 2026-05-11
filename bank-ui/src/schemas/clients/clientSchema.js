import { z } from 'zod';

export const clientSchema = z.object({
    
    name: z
        .string()
        .min(1, "Name is required")
        .max(50, "Max 50 characters"),
    
    email: z
        .string()
        .min(1, "Email is required")
        .email("Invalid email"),
    
    phoneNumber: z
        .string()
        .min(1, "Phone number is required")
        .regex(/^\+\d{10,15}$/, "Invalid phone number")
})