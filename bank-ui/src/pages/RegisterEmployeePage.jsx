import { useState } from "react";
import { api } from "../api/api";
function RegisterEmployeePage() {
    
    const[inputEmail, setEmail] = useState("");
    const [inputPassword, setPassword] = useState("");
    const [role, setRole] = useState(0);

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
                    userRole: roleMap[role]}
            );

            console.log(res.data);

            setEmail("");
            setPassword("");
            setRole("Employee");

        } catch (error){
            console.log(error);
        }
    }

    return (
        <div style={{ padding: "2px" }}>

            <div style={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',}}>
                <h1>Register new employee</h1>

                <div style={{
                    display: "flex",
                    flexDirection: "column",
                    gap:"10px",
                    marginBottom:"20px",
                    width: "20%"
                }}>
                    <input
                        className="create-client-input"
                        style={{ width: "100%", padding: "8px", margin: "0 auto" }}
                        value={inputEmail}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Enter email"
                    />

                    <input
                        className="create-client-input"
                        style={{ width: "100%", padding: "8px", margin: "0 auto" }}
                        value={inputPassword}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Enter password"
                    />

                    <select 
                        value={role} 
                        onChange={(e) => setRole(Number(e.target.value))} 
                        style={{ 
                            padding: "10px", 
                            margin: "0 auto", 
                            borderRadius: "12px", 
                            border: "1px solid #e5e7eb", 
                            backgroundColor: "#fff" 
                    }}
                    >
                        <option value={0}>Admin</option>
                        <option value={1}>Employee</option>
                    </select>

                    <button
                        className="primary-btn"
                        style={{ width: "50%", padding: "8px", margin: "0 auto" }}
                        onClick={createUser}
                        disabled={!inputEmail || !inputPassword}>
                        Create
                    </button>
                </div>
            </div>
        </div>
    )
}

export default RegisterEmployeePage;