import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { api } from "../../api/api";
function CreateClient() {
    
    const navigate = useNavigate();
    
    const[inputName, setName] = useState("");
    const[inputEmail, setEmail] = useState("");
    const [inputPhoneNumber, setPhoneNumber] = useState("");

    const styles = {
        card: {
            backgroundColor: "#ffffff",
            border: "1px solid #e5e7eb",
            borderRadius: "8px",
            padding: "20px",
            boxShadow: "0 1px 3px rgba(0,0,0,0.05)"
        }
    };
    const createClient = async () => {
        if(inputName.trim() === "" 
            || inputEmail.trim() === "" 
            || inputPhoneNumber.trim() === ""
        ) return;
        
        try{
            const res = await api.post(
                "/clients",{
                    name: inputName,
                    email: inputEmail,
                    phoneNumber: inputPhoneNumber}
            );
            
            console.log(res.data);
            
            setName("");
            setEmail("");
            setPhoneNumber("");
            
        } catch (error){
            console.log(error);
        }
    }
    
    return (
        <div 
            style={{
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
                gap: "20px"
            }}>
                
            <h1 style={{
                textAlign: "center",
                margin: 0,
                fontSize: "36px",
                fontWeight: "600"
            }}
            >
                Create new client
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
                    value={inputName} 
                    onChange={(e) => setName(e.target.value)} 
                    placeholder="Enter name"
                />
                
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
                        color: "#1f2937"
                    }}
                    value={inputPhoneNumber} 
                    onChange={(e) => setPhoneNumber(e.target.value)} 
                    placeholder="Enter phone number"
                />
                
                <button
                    className="primary-btn"
                    style={{
                        padding: "14px",
                        borderRadius: "12px",
                        fontSize: "16px"
                    }}
                    onClick={createClient} 
                    disabled={!inputName || !inputEmail || !inputPhoneNumber}>
                    Create
                </button>
            </div>
    </div>
    )
}

export default CreateClient;