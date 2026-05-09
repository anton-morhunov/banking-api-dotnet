import { useState } from "react";
import { api } from "../api/api";
function CreateAccountPage() {

    const[inputClientId, setClientId] = useState("");
    const [type, setType] = useState("0");

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
        if(inputClientId.trim() === "") return;

        try{
            const res = await api.post(
                "/accounts",{
                    clientId: inputClientId,
                    accountType: Number(type)}
            );

            console.log(res.data);

            setClientId("");
            setType("0");

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
            }}
        >
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
                        fontWeight: "600"
                    }}
                >
                    Create Account
                </h1>
                
                <input
                    value={inputClientId}
                    onChange={(e) => setClientId(e.target.value)}
                    placeholder="Enter client id"
                    style={{
                        padding: "14px",
                        borderRadius: "12px",
                        border: "1px solid #ddd",
                        fontSize: "16px",
                        backgroundColor: "#ffffff",
                        color: "#1f2937"
                    }}
                />

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
                    onClick={createUser}
                    className="primary-btn"
                    style={{
                        padding: "14px",
                        borderRadius: "12px",
                        fontSize: "16px"
                    }}
                >
                    Create
                </button>
            </div>
        </div>
    );
}

export default CreateAccountPage;