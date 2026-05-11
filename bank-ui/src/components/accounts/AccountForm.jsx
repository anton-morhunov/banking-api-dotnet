import { useState } from "react";
import { api } from "../../api/api";
import { useForm } from "react-hook-form";
import {AccountSchema} from "../../schemas/accounts/accountSchema.js";
import { zodResolver } from "@hookform/resolvers/zod";
function AccountForm() {
    
    const [successMessage, setSuccessMessage] = useState("");
    const [type, setType] = useState("0");
    
    const{
        register,
        handleSubmit,
        reset,
        formState: { errors, isSubmitting, touchedFields},
    } = useForm({
        resolver: zodResolver(AccountSchema)
    })
    
    const createAccount = async (data) => {
        try{
            const res = await api.post(
                "/accounts",{
                    clientId: data.clientId,
                    accountType: Number(type)}
            );

            console.log(res.data);
            setSuccessMessage("Account successfully created");
            
            reset();

            setTimeout(() => {
                    setSuccessMessage("");
                }, 4000);

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
            <div
                style={{
                    width: "400px",
                    backgroundColor: "#fff",
                    padding: "40px",
                    borderRadius: "20px",
                    boxShadow: "0 4px 12px rgba(0,0,0,0.08)",
                    display: "flex",
                    flexDirection: "column",
                    gap: "20px"
                }}
            >
                <h1
                    style={{
                        textAlign: "center",
                        margin: 0,
                        fontSize: "36px",
                        fontWeight: "600",
                        marginBottom: "20px",
                    }}
                >
                    Create Account
                </h1>
                
                <form 
                    onSubmit={handleSubmit(createAccount)}
                      style={{
                          display: "flex",
                          flexDirection: "column",
                          gap: "20px" 
                }}
                >
                    <div>
                        <input 
                            placeholder="Enter client id"
                            {...register("clientId")} 
                            style={{
                                width: "370px",
                                padding: "14px", 
                                borderRadius: "12px", 
                                border: 
                                    errors.clientId 
                                        ? "2px solid red" 
                                        : touchedFields.clientId 
                                            ? "2px solid green" 
                                            : "1px solid #ddd", 
                                fontSize: "16px", 
                                backgroundColor: "#ffffff", 
                                color: "#1f2937"
                        }}
                        />
                        {errors.clientId && (
                            <p style={{ 
                                color: "red", 
                                fontSize: "14px", 
                                marginTop: "5px"
                            }}
                            >
                                {errors.clientId.message}
                            </p>
                        )}
                    
                    </div>
                    <select 
                        value={type} 
                        onChange={(e) => setType(e.target.value)} 
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
                        <option value="0">Debit</option>
                        <option value="1">Credit</option>
                        <option value="2">Transfer</option>
                    </select>
                    
                    <button 
                        type="submit" 
                        disabled={isSubmitting} 
                        className="primary-btn" 
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
    );
}

export default AccountForm;