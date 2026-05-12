import { useState } from "react";
import { api } from "../../../api/api";
import {employeeSchema} from "../../../schemas/employees/employeeSchema.js";
import {zodResolver} from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
function RegisterEmployeeForm() {
    
    const [successMessage, setSuccessMessage] = useState("");
    const [role, setRole] = useState("0");
    
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isSubmitting, touchedFields},
    } = useForm({
        resolver: zodResolver(employeeSchema)
    });
    
    const createUser = async (data) => {
        try{
            const res = await api.post(
                "/auth/register",{
                    email: data.email,
                    password: data.password,
                    userRole: Number(role)}
            );

            console.log(res.data);
            setSuccessMessage("User successfully registered");
            
            reset();
            
            setTimeout(() => 
                setSuccessMessage(""),
                4000);
        } catch (error){
            console.log(error);
        }
    }

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
                gap: "20px"}}>

                <h1 style={{
                    textAlign: "center",
                    margin: 0,
                    fontSize: "36px",
                    fontWeight: "600",
                    marginBottom: "20px",
                }}
                >
                    Register new employee
                </h1>
                <form 
                    onSubmit={handleSubmit(createUser)} 
                    style={{
                        display: "flex", 
                        flexDirection: "column", 
                        gap: "20px" 
                }}
                >
                    <div>
                        <input
                            placeholder="Enter email"
                            {...register("email")}
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
                        />
                        {errors.email && (
                            <p 
                                style={{
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
                            placeholder="Enter password"
                            {...register("password")}
                            style={{
                                width: "370px",
                                padding: "14px", 
                                borderRadius: "12px", 
                                border: 
                                    errors.password 
                                        ? "2px solid red" 
                                        : touchedFields.password 
                                            ? "2px solid green" 
                                            : "1px solid #ddd", 
                                fontSize: "16px", 
                                backgroundColor: "#ffffff", 
                                color: "#1f2937",
                            }}
                />
                        {errors.password && (
                            <p style={{
                                color: "red",
                                fontSize: "14px",
                                marginTop: "5px"
                            }}
                            >
                                {errors.password.message}
                            </p>
                        )}
                    </div>
                    <select 
                        value={role} 
                        onChange={(e) => setRole(e.target.value)} 
                        style={{
                            padding: "14px", 
                            borderRadius: "12px", 
                            border: "1px solid #ddd", 
                            fontSize: "16px", 
                            backgroundColor: "#fff", 
                            width: "100%", 
                            color: "#1f2937", 
                            textAlign: "center", 
                            fontWeight: "500"
                    }}
                    >
                        <option value={"0"}>Admin</option>
                        <option value={"1"}>Employee</option>
                    </select>
                    <button 
                        type="submit"
                        className="primary-btn" 
                        style={{
                            padding: "14px", 
                            borderRadius: "12px", 
                            fontSize: "16px"
                    }}
                        disabled={isSubmitting
                    }
                    >
                        Create
                    </button>
                </form>
            </div>
        </div>
    );
}

export default RegisterEmployeeForm;