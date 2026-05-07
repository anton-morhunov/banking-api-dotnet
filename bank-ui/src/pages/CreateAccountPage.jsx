import { useState } from "react";
import { api } from "../api/api";
function CreateAccountPage() {

    const[inputClientId, setClientId] = useState("");
    const [type, setType] = useState(0);

    const typeMap = {
        Debit: 0,
        Credit: 1,
        Transfer: 2
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
        if(inputClientId.trim() === "") return;

        try{
            const res = await api.post(
                "/accounts",{
                    clientId: inputClientId,
                    accountType: typeMap[type]}
            );

            console.log(res.data);

            setClientId("");
            setType("Debit");

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
                <h1>Create new account</h1>

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
                        value={inputClientId}
                        onChange={(e) => setClientId(e.target.value)}
                        placeholder="Enter client id"
                    />

                    <select
                        value={type}
                        onChange={(e) => setType(Number(e.target.value))}
                        style={{
                            padding: "10px",
                            margin: "0 auto",
                            borderRadius: "12px",
                            border: "1px solid #e5e7eb",
                            backgroundColor: "#fff"
                        }}
                    >
                        <option value={0}>Debit</option>
                        <option value={1}>Credit</option>
                        <option value={2}>Transfer</option>
                    </select>

                    <button
                        className="primary-btn"
                        style={{ width: "50%", padding: "8px", margin: "0 auto" }}
                        onClick={createUser}
                        disabled={!inputClientId}>
                        Create
                    </button>
                </div>
            </div>
        </div>
    )
}

export default CreateAccountPage;