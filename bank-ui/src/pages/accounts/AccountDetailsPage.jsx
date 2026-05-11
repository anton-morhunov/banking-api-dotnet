import {useParams} from 'react-router-dom';
import {useEffect, useState} from 'react';
import {api} from '../../api/api';

function AccountDetails() {
    
    const {id} = useParams();
    const [account, setAccount] = useState({});
    const [client, setClient] = useState({});
    
    const styles ={
        card: {
            backgroundColor: "#ffffff",
            border: "1px solid #e5e7eb",
            borderRadius: "8px",
            padding: "20px",
            boxShadow: "0 1px  3px rgba(0,0,0,0.5)",
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
    }
    
    const sectionStyle = {
        width: '40%',
    }
    
    const rowStyle = {
        display: 'flex',
        justifyContent: 'space-between',
        marginBottom: '12px',
    }
    
    const labelStyle = {
        color: "#666"
    }
    
    const valueStyle = {
        fontWeight: "500"
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

    const statusMap = {
        0: "Active",
        1: "Blocked",
        2: "Closed",
    }
    
    useEffect(() => {
        
        const fetchData = async () => {
            try{
                var response = await api.get(`/accounts?accountId=${id}`);
                setAccount(response.data);
            } catch (error) {
                console.log(error);
            }
        };
        fetchData();
    }, [id]);
    
    useEffect(() => {
        api.get(`/clients/${id}`)
            .then(response => setClient(response.data))
            .catch(error => console.log(error));
    })
    
    if(!account) return <div className="loader"></div>;
    
    return(
        <div style={{padding: '30px'}}>
            <div style={{...styles.card, marginBottom: '30px'}}>
                <h1 style={{
                    textAlign: "center",
                    fontSize: '32px',
                    fontWeight: '400',
                    marginBottom: '20px',
                }}>
                    {account.accountId}
                </h1>
                <h2 style={{
                    textAlign: "center",
                    fontSize: '32px',
                    fontWeight: '400',
                    marginBottom: '40px',
                }}>
                    Account Details
                </h2>
                <div style={{flex: 1}}>
                    <div style={{
                        display:"flex", 
                        justifyContent:"space-between"}}>
                        <div style={sectionStyle}>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Account number</span>
                                <span style={valueStyle}>{account.accountNumber}</span>
                            </div>
                            
                            <div style={rowStyle}>
                                <span style={labelStyle}>Balance</span>
                                <span style={valueStyle}>{account.balance}</span>
                            </div>
                            
                            <div style={rowStyle}>
                                <span style={labelStyle}>Created At</span>
                                <span style={valueStyle}>{new Date(account.createdAt).toLocaleString()}</span>
                            </div>
                        </div>
                        <div style={sectionStyle}>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Account Plan</span>
                                <span style={valueStyle}>
                                    <span style={{
                                        color: account.plan === 0
                                            ? "#64748b"
                                            : account.plan === 1
                                                ? "#eab308"
                                                : "#0f766e",
                                        fontWeight: "bold"
                                    }}>
                                        {accountPlan[account.plan]}
                                    </span>
                                </span>
                                <button
                                    type="button"
                                    className="primary-btn">
                                    Edit
                                </button>
                            </div>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Account Type</span>
                                <span style={valueStyle}>
                                    <span style={{
                                        color: account.accountType === 0
                                            ? "#2563eb"
                                            : account.accountType === 1
                                                ? "#22c55e"
                                                : "#9333ea",
                                        fontWeight: "bold"
                                    }}>{accountType[account.accountType]}
                                        </span>
                                </span>
                                <button
                                    type="button"
                                    className="primary-btn">
                                    Edit
                                </button>
                            </div>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Account Status</span>
                                <span style={{
                                    color: account.status === 0 ? "green" : "red",
                                    fontWeight: "bold"
                                }}>
                                        {statusMap[account.status]} 
                                    </span>
                                <button 
                                    type="button" 
                                    className="primary-btn"
                                >
                                    Edit
                                </button>
                            </div>
                            <div 
                                style={{
                                    ...rowStyle, 
                                    display: "flex", 
                                    justifyContent:"center"
                            }}
                            >
                                <span 
                                    style={labelStyle}
                                >
                                    <button 
                                        className="block-btn"
                                    >
                                        Block
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div 
                style=
                    {{
                        ...styles.card,
                        marginBottom: "30px"
            }}
            >
                <h1 style={{
                    textAlign: "center",
                    fontSize: '32px',
                    fontWeight: '400',
                    marginBottom: '20px'
                }}>
                    Connected Accounts
                </h1>
                <table 
                    cellPadding="5" 
                    style={{width:"100%", 
                        marginTop:"30px", 
                        justifyContent:"center"
                }}
                >
                    <thead>
                    <tr>
                        <th style={styles.th}>ID</th>
                        <th style={styles.th}>Name</th>
                        <th style={styles.th}>Email</th>
                        <th style={styles.th}>Phone number</th>
                        <th style={styles.th}>Status</th>
                        <th style={styles.th}>Created date</th>
                    </tr>
                    </thead>
                </table>
            </div>
        </div>
    )
}

export default AccountDetails;