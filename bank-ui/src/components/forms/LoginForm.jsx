import { useForm } from 'react-hook-form';
import { zodResolver } from "@hookform/resolvers/zod";
import { LoginSchema } from "../../schemas/loginSchema.js";
import { useNavigate } from "react-router-dom";
import { login } from '../../services/authServices.js';
import { useState } from 'react';
function LoginForm() {

    const navigate = useNavigate();
    const[loginError, setLoginError] = useState("");
    
    const { 
        register, 
        handleSubmit, 
        formState: { errors, isSubmitting, touchedFields},
    } = useForm({
        resolver: zodResolver(LoginSchema)
    });
    const handleLogin = async (data) => {
        setLoginError("");
        try{
            await login(data.email, data.passwordHash);
            navigate("/home");
        }catch(error){
            setLoginError("Your email or password is incorrect. Please try again.");
        }
    }
    
    return(
            <div style={{
                width: "400px",
                backgroundColor: "#fff",
                padding: "40px",
                borderRadius: "20px",
                boxShadow: "0 4px 12px rgba(0,0,0,0.08)",
                display: "flex",
                flexDirection: "column",
                gap: "30px"
            }}>
                <h1 style={{
                    textAlign: "center",
                    margin: 0,
                    fontSize: "36px",
                    fontWeight: "600"
                }}
                >
                    TheNorthFieldBank
                </h1>

                <h2 style={{
                    textAlign: "center",
                    margin: 0,
                    fontSize: "30px",
                    fontWeight: "600"
                }}
                >
                    Back Office
                </h2>
                
                {(errors.email || errors.passwordHash || loginError) && (
                    <div
                        style={{
                            backgroundColor: "rgba(239, 68, 68, 0.15)",
                            color: "#991b1b",
                            padding: "12px",
                            borderRadius: "12px",
                            marginBottom: "20px",
                            textAlign: "center",
                            border: "1px solid rgba(239, 68, 68, 0.3)"
                        }}
                    >
                        {errors.email?.message ||
                            errors.passwordHash?.message ||
                            loginError}
                    </div>
                )}
                
                <form onSubmit={handleSubmit(handleLogin)}>
                    <div style={{
                        display: "flex", 
                        gap: "15px", 
                        marginBottom: "20px", 
                        flexDirection: "column",
                    }}
                    >
                        <div>
                            <input 
                                style={{ 
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
                                    color: "#1f2937", 
                                    width: "370px"}} 
                                placeholder="Enter LogIn"
                                {...register("email")}/>
                        </div>
                        <div>
                            <input 
                                type="password" 
                                style={{ 
                                    padding: "14px", 
                                    borderRadius: "12px", 
                                    border: 
                                        errors.passwordHash 
                                            ? "2px solid red" 
                                            : touchedFields.passwordHash 
                                                ? "2px solid green" 
                                                : "1px solid #ddd", 
                                    fontSize: "16px", 
                                    backgroundColor: "#ffffff", 
                                    color: "#1f2937", 
                                    width: "370px"
                            }} 
                                placeholder="Enter Password"
                                {...register("passwordHash"
                                )}
                            />
                        </div>
                    </div>
                    <button 
                        className="primary-btn" 
                        style={{
                            padding: "14px", 
                            borderRadius: "12px", 
                            fontSize: "16px", 
                            width: "100%"
                    }} 
                        disabled={isSubmitting} 
                        type="submit"
                    >
                        LogIn
                    </button>
                </form>
            </div>
    );
}

export default LoginForm;