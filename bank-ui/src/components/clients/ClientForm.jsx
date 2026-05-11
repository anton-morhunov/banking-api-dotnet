import { api } from "../../api/api";
import { useForm } from 'react-hook-form';
import { zodResolver } from "@hookform/resolvers/zod";
import { clientSchema} from "../../schemas/clients/clientSchema.js";
import { useState } from "react";

function ClientForm() {
    
    const [successMessage, setSuccessMessage] = useState("");
    
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isSubmitting, touchedFields },
    } = useForm({
        resolver: zodResolver(clientSchema)
    });
    
    const createClient = async (data) => {
        try{
            const res = await api.post("/clients", {
                name: data.name,
                email: data.email,
                phoneNumber: data.phoneNumber,
            });
            
            console.log(res.data);
            setSuccessMessage("Client successfully created!");
            
            reset();

            setTimeout(() => {
                setSuccessMessage("");
            }, 4000);
            
        }catch(error){
            console.log(error);
        }
    };
    
    return (
        <div> {successMessage && (
            <div
                style={{
                    backgroundColor: "rgba(34, 197, 94, 0.2)",
                    color: "#166534",
                    padding: "20px",
                    borderRadius: "12px",
                    border: "1px solid rgba(34, 197, 94, 0.4)",
                    textAlign: "top",
                    fontWeight: "500",
                    marginBottom: "20px",
                }}
            >
                {successMessage}
            </div>
        )}

            <div style={{
                width: "400px",
                backgroundColor: "#fff",
                padding: "40px",
                borderRadius: "20px",
                boxShadow: "0 4px 12px rgba(0,0,0,0.08)",
                display: "flex",
                flexDirection: "column",
                gap: "20px"
            }}>

                <h1 style={{
                    textAlign: "center",
                    margin: 0,
                    fontSize: "36px",
                    fontWeight: "600",
                    marginBottom: "20px"
                }}
                >
                    Create new client
                </h1>
                <form 
                    onSubmit={handleSubmit(createClient)} 
                    style={{ 
                        display: "flex", 
                        flexDirection: "column", 
                        gap: "20px" }}>
                    
                    <div>
                        <input 
                            style={{
                                width: "370px", 
                                padding: "14px", 
                                borderRadius: "12px", 
                                border: 
                                    errors.name 
                                        ? "2px solid red" 
                                        : touchedFields.name 
                                            ? "2px solid green" 
                                            : "1px solid #ddd", 
                                fontSize: "16px", 
                                backgroundColor: "#ffffff", 
                                color: "#1f2937", 
                                transition: "0.3s"
                        }} 
                            placeholder="Enter name"
                            {...register("name")}
                        />
                        {errors.name && (
                            <p 
                                style={{ 
                                    color: "red", 
                                    fontSize: "14px", 
                                    marginTop: "5px"
                            }}>
                                {errors.name.message}
                            </p>
                        )}
                    </div>
                    <div>
                        <input
                            style={{
                                width: "370px",
                                padding: "14px",
                                borderRadius: "12px",
                                border:
                                    errors.email
                                        ? "2px solid red"
                                        : touchedFields.email
                                            ? "2px solid green"
                                            : "1px solid #ddd",
                                fontSize: "16px",
                                backgroundColor: "#ffffff",
                                color: "#1f2937"
                            }} 
                            placeholder="Enter email"
                            {...register("email")}
                        />
                        {errors.email && (
                            <p style={{ 
                                color: "red", 
                                fontSize: "14px", 
                                marginTop: "5px"
                            }}
                            >
                                {errors.email.message}
                            </p>
                        )}
                    </div>
                    
                    <div>
                        <input
                            style={{
                                width: "370px",
                                padding: "14px",
                                borderRadius: "12px",
                                border:
                                    errors.phoneNumber
                                        ? "2px solid red"
                                        : touchedFields.phoneNumber
                                            ? "2px solid green"
                                            : "1px solid #ddd",
                                fontSize: "16px",
                                backgroundColor: "#ffffff",
                                color: "#1f2937"
                            }}
                            placeholder="Enter phone number"
                            {...register("phoneNumber")}
                        />
                        {errors.phoneNumber && (
                            <p style={{ color: "red", fontSize: "14px", marginTop: "5px" }}>
                                {errors.phoneNumber.message}
                            </p>
                        )}
                    </div>
                    
                    <button 
                        type="submit" 
                        className="primary-btn" 
                        disabled={isSubmitting} 
                        style={{
                            padding: "14px", 
                            borderRadius: "12px", 
                            fontSize: "16px"
                    }}
                    >
                        Create
                    </button>
                </form>
            </div>
        </div>
    )
}

export default ClientForm;