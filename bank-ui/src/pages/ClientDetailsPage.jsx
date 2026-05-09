import {useParams} from "react-router-dom";
import {useState, useEffect} from "react";
import {api} from "../api/api.js";

function ClientDetailsPage(){
    
    const { id } = useParams();
    const [client, setClient] = useState(null);
    const [account, setAccounts] = useState([]);
    const [editingField, setEditField] = useState(null);
    const [editedClient, setEditedClient] = useState({});

    const styles = {
        card: {
            backgroundColor: "#ffffff",
            border: "1px solid #e5e7eb",
            borderRadius: "8px",
            padding: "20px",
            boxShadow: "0 1px 3px rgba(0,0,0,0.05)"
        },
        th: {
            backgroundColor: "#f9fafb",
            borderBottom: "1px solid #e5e7eb",
            padding: "10px",
            textAlign: "left"
        },
        td: {
            borderBottom: "1px solid #e5e7eb",
            padding: "10px"
        }
    };

    const sectionStyle = {
        width: "40%"
    };

    const rowStyle = {
        display: "flex",
        justifyContent: "space-between",
        marginBottom: "12px"
    };

    const labelStyle = {
        color: "#666"
    };

    const valueStyle = {
        fontWeight: "500"
    };

    const statusMap = {
        0: "Active",
        1: "Blocked",
        2: "Closed",
    }

    const accountPlan = {
        0: "Basic",
        1: "Premium",
        2: "Business",
    }

    const accountType = {
        0: "Debit",
        1: "Credit",
        2: "Transfer"
    }

    useEffect(()=> {
        const fetchClient = async () => {

            try {
                var response = await api.get(`/clients/${id}`);
                setClient(response.data);
                setEditedClient(response.data);
            } catch (err) {
                console.log(err);
            }
        };
        fetchClient();
    }, [id]);

    const saveField = async () => {

        try {
            await api.put(`/clients/${client.id}`, editedClient);

            setClient(editedClient);

            setEditField(null);

        } catch (err) {
            console.log(err);
        }
    };
    
    const updateClientStatus = async () => {
        try{
            await api.patch(`/clients/${client.id}/dto`, {status: status});
            
            setClient({
                ...client,
                status: dto
            })
        } catch(err){
            console.log(err);
        }
    }

    useEffect(() => {
        api.get(`/accounts/${id}`)
            .then(response => setAccounts(response.data))
            .catch(error => console.log(error));
    }, []);
    
    if(!client) return("Loading...");
    
    return(

        <div style={{ padding: "30px" }}>
            <div style={{...styles.card, marginBottom:"30px"}}>

            <h1 style={{
                textAlign: "center",
                fontSize: "32px",
                fontWeight: "400",
                marginBottom: "20px"}}>
                {client.id}
            </h1>
            
            <h2 style={{
                textAlign: "center",
                fontSize: "32px",
                fontWeight: "400",
                marginBottom: "40px"}}>
                Client Details
            </h2>
            
            <div style={{ display: "flex", alignItems: "flex-start", justifyContent: "space-between" }}>

                <div style={{ flex: 1 }}>
                    <div style={{
                        display: "flex",
                        justifyContent: "space-between"
                    }}>
                        <div style={sectionStyle}>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Name</span>
                                {
                                    editingField === "name" ? (
                                        <input
                                            value={editedClient.name}
                                            onChange={(e) =>
                                                setEditedClient({
                                                    ...editedClient,name: e.target.value
                                                })
                                            }
                                            style={{
                                                border: "none",
                                                borderBottom: "2px solid #d1d5db",
                                                background: "transparent",
                                                outline: "none",
                                                padding: "6px 2px",
                                                fontSize: "18px",
                                                width: "220px",
                                                color: "#374151",
                                                fontWeight: "500",
                                                caretColor: "#2563eb"
                                            }}
                                        />
                                    ) : (
                                        <span style={valueStyle}>{client.name}</span>
                                    )}
                                {editingField === "name" ? (
                                    <button 
                                        type="button" 
                                        onClick={()=>saveField("name")} 
                                        className="primary-btn">
                                        Save
                                    </button>
                                ) :( 
                                    <button 
                                        type="button" 
                                        onClick={() => setEditField("name")} 
                                        className="primary-btn">
                                    Edit
                                    </button>
                                )}
                            </div>

                            <div style={rowStyle}>
                                <span style={labelStyle}>Email</span>
                                {editingField === "email" ? (
                                    <input
                                    value={editedClient.email}
                                    onChange={(e) => 
                                        setEditedClient({
                                            ...editedClient, email: e.target.value
                                        })
                                    }
                                    style={{
                                        border: "none",
                                        borderBottom: "2px solid #d1d5db",
                                        background: "transparent",
                                        outline: "none",
                                        padding: "6px 2px",
                                        fontSize: "18px",
                                        width: "220px",
                                        color: "#374151",
                                        fontWeight: "500",
                                        caretColor: "#2563eb"
                                    }}
                                    />
                                ) : (
                                    <span style={valueStyle}>{client.email}</span>
                                )}
                                {editingField === "email" ? (
                                    <button
                                        type="button"
                                        onClick={()=>saveField("email")}
                                        className="primary-btn">
                                        Save
                                    </button>
                                ) :(
                                    <button
                                        type="button"
                                        onClick={() => setEditField("email")}
                                        className="primary-btn">
                                        Edit
                                    </button>
                                )}
                            </div>

                            <div style={rowStyle}>
                                <span style={labelStyle}>Phone number</span>
                                {editingField === "phoneNumber" ? (
                                    <input
                                    value={editedClient.phoneNumber}
                                    onChange={(e) => 
                                        setEditedClient({
                                            ...editedClient, phoneNumber: e.target.value
                                        })
                                    }
                                    style={{
                                        border: "none",
                                        borderBottom: "2px solid #d1d5db",
                                        background: "transparent",
                                        outline: "none",
                                        padding: "6px 2px",
                                        fontSize: "18px",
                                        width: "220px",
                                        color: "#374151",
                                        fontWeight: "500",
                                        caretColor: "#2563eb"
                                    }}
                                    />
                                ) : (
                                    <span style={valueStyle}>{client.phoneNumber}</span>
                                )}
                                {editingField === "phoneNumber" ? (
                                    <button
                                        type="button"
                                        onClick={()=>saveField("phoneNumber")}
                                        className="primary-btn">
                                        Save
                                    </button>
                                ) :(
                                    <button
                                        type="button"
                                        onClick={() => setEditField("phoneNumber")}
                                        className="primary-btn">
                                        Edit
                                    </button>
                                )}
                            </div>
                        </div>
                        
                        <div style={sectionStyle}>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Created At</span>
                                <span style={valueStyle}>{new Date(client.created).toLocaleString()}</span>
                            </div>

                            <div style={rowStyle}>
                                <span style={labelStyle}>Status</span>
                                <span style={valueStyle}>
                                    <span style={{
                                        color: client.status === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {statusMap[client.status]}
                                    </span>
                                </span>
                            </div>
                            <div style={{...rowStyle, display: "flex", justifyContent: "center"}}>
                                <span style={labelStyle}>
                                    <button className="block-btn">
                                        Block
                                    </button>
                                </span>
                            </div>
                            
                            <div style={{...rowStyle, display: "flex", justifyContent: "center"}}>
                                <span style={labelStyle}>
                                    <button className="block-btn">
                                        Suspend
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                
            </div>
            <div style={styles.card}>
                <h1 style={{
                    textAlign: "center",
                    fontSize: "32px",
                    fontWeight: "400",
                    marginBottom: "40px"}}>
                    Accounts
                </h1>

                <table
                    cellPadding="5"
                    style={{ width: "100%", marginTop: "30px", justifyContent: "center"}}
                >
                    <thead>
                    <tr>
                        <th style={styles.th}>ID</th>
                        <th style={styles.th}>Balance</th>
                        <th style={styles.th}>Client ID</th>
                        <th style={styles.th}>Status</th>
                        <th style={styles.th}>Account Plan</th>
                        <th style={styles.th}>Account Type</th>
                        <th style={styles.th}>Created date</th>
                    </tr>
                    </thead>

                    <tbody>
                    {account?.map(account => (
                        <tr key={account.id}>
                            <td style={styles.td}>{account.accountId}</td>
                            <td style={styles.td}>{account.balance}</td>
                            <td style={styles.td}>{account.clientId}</td>
                            <td style={styles.td}>
                                    <span style={{
                                        color: account.status === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {statusMap[account.status]}
                                    </span>
                            </td>
                            <td style={styles.td}>
                                        <span style={{
                                            color: account.plan === 0 ? "blue" : "gold",
                                            fontWeight: "bold"
                                        }}>
                                            {accountPlan[account.plan]}
                                            </span>
                            </td>
                            <td style={styles.td}>
                                        <span style={{
                                            color: account.accountType === 0
                                                ? "#2563eb"
                                                : account.accountType === 1
                                                    ? "#22c55e"
                                                    : "#9333ea",
                                            fontWeight: "bold"
                                        }}>{accountType[account.accountType]}
                                        </span>
                            </td>
                            <td style={styles.td}>
                                {new Date(account.createdAt).toLocaleString()}
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default ClientDetailsPage;