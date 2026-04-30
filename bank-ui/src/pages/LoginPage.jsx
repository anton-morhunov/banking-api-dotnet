import {useNavigate} from 'react-router-dom'

function LoginPage() {
    
    const navigate = useNavigate();

    const styles = {
        card: {
            backgroundColor: "#ffffff",
            border: "1px solid #e5e7eb",
            borderRadius: "20px",
            padding: "40px",
            boxShadow: "0 50px 40px rgba(0,0,0,0.10)"
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
                        className="login-input" 
                        style={{ width: "100%", padding: "8px", margin:"0 auto"}} 
                        placeholder="Enter LogIn"/>
                    
                    <input 
                        className="login-input" 
                        style={{ width: "100%", padding: "8px", margin:"0 auto" }} 
                        placeholder="Enter Password"/>
                </div>
                <button
                    className="primary-btn" 
                    onClick={() => navigate("/home")}>
                    LogIn
                </button>
            </div>
        </div>
    );
}

export default LoginPage;