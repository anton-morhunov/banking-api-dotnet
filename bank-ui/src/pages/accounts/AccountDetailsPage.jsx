import {useParams} from 'react-router-dom';
import {useEffect, useState} from 'react';
import {api} from '../../api/api';
import BlockButton from "../../components/ui/Button/BlockButton.jsx";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import CardDetailsPage from "../../components/ui/Card/CardDetailsPage.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import { accountPlan, accountType, accountStatusMap} from "../../constants/accountConstants.js";

function AccountDetails() {
    
    const {id} = useParams();
    const [account, setAccount] = useState({});
    const [client, setClient] = useState({});
    
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
            <CardDetailsPage>
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
                                <PrimaryButton
                                    type="button"
                                >
                                    Edit
                                </PrimaryButton>
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
                                <PrimaryButton
                                    type="button"
                                >
                                    Edit
                                </PrimaryButton>
                            </div>
                            <div style={rowStyle}>
                                <span style={labelStyle}>Account Status</span>
                                <span style={{
                                    color: account.status === 0 ? "green" : "red",
                                    fontWeight: "bold"
                                }}>
                                        {accountStatusMap[account.status]} 
                                    </span>
                                <PrimaryButton
                                    type="button"
                                >
                                    Edit
                                </PrimaryButton>
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
                                    <BlockButton>
                                        Block
                                    </BlockButton>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </CardDetailsPage>
            <TableCard>
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
                        <TableColumn>ID</TableColumn>
                        <TableColumn>Name</TableColumn>
                        <TableColumn>Email</TableColumn>
                        <TableColumn>Phone number</TableColumn>
                        <TableColumn>Status</TableColumn>
                        <TableColumn>Created date</TableColumn>
                    </tr>
                    </thead>
                </table>
            </TableCard>
        </div>
    )
}

export default AccountDetails;