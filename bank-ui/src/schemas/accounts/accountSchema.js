import {z} from "zod";

export const AccountSchema = z.object({
    clientId: z
        .string()
        .min(1, "Client ID is required"),
});