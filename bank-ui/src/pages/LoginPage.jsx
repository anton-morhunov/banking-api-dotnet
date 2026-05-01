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
            height: '100vh',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            backgroundColor: "#f5f6f8"
        }}>
            <div style={{
                ...styles.card,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                maxWidth: '100%',
                width: '20%',
            }}>
                <h1 style={{
                    fontSize: "32px", 
                    fontWeight: "600"}}>
                    BankAPI Back Office
                </h1>
                
                <div style={{
                    display: "flex", 
                    gap: "10px", 
                    marginBottom: "20px", 
                    flexDirection: "column",
                }}>
                    <input
                        value={inputLogin}
                        onChange={(e) => setInputLogin(e.target.value)}
                        className="login-input" 
                        style={{ width: "100%", padding: "8px", margin:"0 auto"}} 
                        placeholder="Enter LogIn"/>
                    
                    <input 
                        type="password"
                        value={inputPassword}
                        onChange={(e) => setInputPassword(e.target.value)}
                        className="login-input" 
                        style={{ width: "100%", padding: "8px", margin:"0 auto" }} 
                        placeholder="Enter Password"/>
                </div>
                <button
                    className="primary-btn" 
                    onClick={handleLogin}>
                    LogIn
                </button>
            </div>
        </div>
    );
}

export default LoginPage;