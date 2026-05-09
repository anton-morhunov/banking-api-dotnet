import { useState } from "react";
import { api } from "../api/api";
function RegisterEmployeePage() {
    
    const[inputEmail, setEmail] = useState("");
    const [inputPassword, setPassword] = useState("");
    const [role, setRole] = useState("0");

    const roleMap = {
        Admin: 0,
        Employee: 1
    };
    
    const styles = {
        card: {
            backgroundColor: "#ffffff",
            border: "1px solid #e5e7eb",
            borderRadius: "8px",
            padding: "20px",
            boxShadow: "0 1px 3px rgba(0,0,0,0.05)"
        }
    };
    const createUser = async () => {
        if(inputEmail.trim() === ""
            || inputPassword.trim() === ""
        ) return;

        try{
            const res = await api.post(
                "/auth/register",{
                    email: inputEmail,
                    password: inputPassword,
                    userRole: Number(role)}
            );

            console.log(res.data);

            setEmail("");
            setPassword("");
            setRole("0");

        } catch (error){
            console.log(error);
        }
    }

    return (
        <div style={{ 
            padding: "2px",
            minHeight: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center" }}>

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
                    fontWeight: "600"
                }}
                >
                    Register new employee
                </h1>
                
                    <input
                        style={{
                            padding: "14px",
                            borderRadius: "12px",
                            border: "1px solid #ddd",
                            fontSize: "16px",
                            backgroundColor: "#ffffff",
                            color: "#1f2937"
                        }}
                        value={inputEmail}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Enter email"
                    />

                    <input
                        style={{ 
                            padding: "14px",
                            borderRadius: "12px",
                            border: "1px solid #ddd",
                            fontSize: "16px",
                            backgroundColor: "#ffffff",
                            color: "#1f2937",
                    }}
                        value={inputPassword}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Enter password"
                    />

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
                        className="primary-btn"
                        style={{ 
                            padding: "14px",
                            borderRadius: "12px",
                            fontSize: "16px" 
                    }}
                        onClick={createUser}
                        disabled={!inputEmail || !inputPassword}>
                        Create
                    </button>
            </div>
        </div>
    )
}

export default RegisterEmployeePage;