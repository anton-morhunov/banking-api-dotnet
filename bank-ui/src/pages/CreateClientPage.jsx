import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { api } from "../api/api";
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
        <div style={{ padding: "2-px" }}>
            
            <div style={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',}}>
            <h1>Create new client</h1>
            
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
                    value={inputName} 
                    onChange={(e) => setName(e.target.value)} 
                    placeholder="Enter name"
                />
                
                <input
                    className="create-client-input"
                    style={{ width: "100%", padding: "8px", margin: "0 auto" }}
                    value={inputEmail} 
                    onChange={(e) => setEmail(e.target.value)} 
                    placeholder="Enter email"
                />
                
                <input
                    className="create-client-input"
                    style={{ width: "100%", padding: "8px", margin: "0 auto"}}
                    value={inputPhoneNumber} 
                    onChange={(e) => setPhoneNumber(e.target.value)} 
                    placeholder="Enter phone number"
                />
                
                <button
                    className="primary-btn"
                    style={{ width: "50%", padding: "8px", margin: "0 auto" }}
                    onClick={createClient} 
                    disabled={!inputName || !inputEmail || !inputPhoneNumber}>
                    Create
                </button>
            </div>
            </div>
    </div>
    )
}

export default CreateClient;