import {useParams} from "react-router-dom";
import {useState, useEffect} from "react";
import {api} from "../api/api.js";

function ClientDetailsPage(){
    
    const { id } = useParams();
    const [client, setClient] = useState(null);
    const [account, setAccounts] = useState([]);

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
        width: "45%"
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
            } catch (err) {
                console.log(err);
            }
        };
        fetchClient();
    }, [id]);

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
                                <span style={valueStyle}>{client.name}</span>
                            </div>

                            <div style={rowStyle}>
                                <span style={labelStyle}>Email</span>
                                <span style={valueStyle}>{client.email}</span>
                            </div>

                            <div style={rowStyle}>
                                <span style={labelStyle}>Phone number</span>
                                <span style={valueStyle}>{client.phoneNumber}</span>
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
                                    <botton className="block-btn">
                                        Block
                                    </botton>
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
                                            color: account.accountType === 0 ? "green" : "orange",
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