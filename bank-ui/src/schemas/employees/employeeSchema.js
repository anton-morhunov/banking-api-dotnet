import { z } from "zod";

export const employeeSchema = z.object({
    email: 
        z.string()
            .min(1, "Email is required.")
            .email("Invalid email address."),
    
    password: 
        z.string()
            .min(1, "Password is required."),
})