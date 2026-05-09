import {useNavigate} from 'react-router-dom'
import { login } from '../services/authServices.js'
import { useState } from 'react';
function LoginPage() {
    
    const navigate = useNavigate();

    const [inputLogin, setInputLogin] = useState("");
    const [inputPassword, setInputPassword] = useState("");

    const handleLogin = async () => {
        try{
            await login(inputLogin, inputPassword);
            navigate("/home");
        }catch(error){
            console.log("Login failed");
        }
    }
    const styles = {
        card: {
            backgroundColor: "#ffffff",
            borderRadius: "20px",
            padding: "40px",
            boxShadow: "0 50px 40px rgba(0,0,0,0.10)",
            width: "20%"
        }
    };

    return(
        <div style={{
            minHeight: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center"
        }}>
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
                
                <div style={{
                    display: "flex", 
                    gap: "15px", 
                    marginBottom: "20px", 
                    flexDirection: "column",
                }}>
                    <input
                        value={inputLogin}
                        onChange={(e) => setInputLogin(e.target.value)}
                        style={{ padding: "14px",
                            borderRadius: "12px",
                            border: "1px solid #ddd",
                            fontSize: "16px",
                            backgroundColor: "#ffffff",
                            color: "#1f2937"}} 
                        placeholder="Enter LogIn"/>
                    
                    <input 
                        type="password"
                        value={inputPassword}
                        onChange={(e) => setInputPassword(e.target.value)}
                        style={{ padding: "14px",
                            borderRadius: "12px",
                            border: "1px solid #ddd",
                            fontSize: "16px",
                            backgroundColor: "#ffffff",
                            color: "#1f2937"}}
                        placeholder="Enter Password"/>
                </div>
                <button
                    className="primary-btn"
                    style={{
                        padding: "14px",
                        borderRadius: "12px",
                        fontSize: "16px"
                    }}
                    onClick={handleLogin}>
                    LogIn
                </button>
            </div>
        </div>
    );
}

export default LoginPage;