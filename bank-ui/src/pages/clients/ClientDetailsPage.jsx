import {useParams, Link} from "react-router-dom";
import {useState, useEffect} from "react";
import {api} from "../../api/api";
import BlockButton from "../../components/ui/Button/BlockButton.jsx";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import CardDetailsPage from "../../components/ui/Card/CardDetailsPage.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import TableCell from "../../components/ui/Card/TableCell.jsx";
import { accountPlan, accountType, accountStatusMap} from "../../constants/accountConstants.js"
import { clientStatusMap } from "../../constants/clientConstants.js"
import UnblockButton from "../../components/ui/Button/UnblockButton.jsx";

function ClientDetailsPage(){
    
    const { id } = useParams();
    const [client, setClient] = useState(null);
    const [account, setAccounts] = useState([]);
    const [editingField, setEditField] = useState(null);
    const [editedClient, setEditedClient] = useState({});
    const [blockClient, setBlockClient] = useState({});

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
        fontWeight: "500",
        color: "#111827",
        textAlign: "right"
    };

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
            const response = await api.put(`/clients/${client.id}`, editedClient);
            
            console.log(response.data);

            setClient(editedClient);

            setEditField(null);

        } catch (err) {
            console.log(err.response);
        }
    };

    useEffect(() => {
        api.get(`/accounts/client/${id}`)
            .then(response => setAccounts(response.data))
            .catch(error => console.log(error));
    }, []);
    
    const updateClientStatus = async (status) => {
        try{
            
            const response = await api.patch(`/clients/${client.id}?dto=${status}`);

            console.log(response.data);
            
            setClient({
                ...client,
                status
            })
        } catch(err){
            console.log(err.response);
        }
    }

    useEffect(() => {
        api.get(`/accounts/client/${id}`)
            .then(response => setAccounts(response.data))
            .catch(error => console.log(error));
    }, []);
    
    if(!client) return <div className="loader"></div>;
    
    return(

        <div style={{ padding: "30px" }}>
            <CardDetailsPage>
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
                marginBottom: "40px"
            }}
            >
                Client Details
            </h2>
            <div 
                style={{ 
                    display: "flex", 
                    alignItems: "flex-start", 
                    justifyContent: "space-between" 
            }}
            >
                <div style={{ flex: 1 }}>
                    <div style={{
                        display: "flex",
                        justifyContent: "space-between"
                    }}
                    >
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
                                    <PrimaryButton 
                                        type="button" 
                                        onClick={()=>saveField("name")}
                                    >
                                        Save
                                    </PrimaryButton>
                                ) :( 
                                    <PrimaryButton 
                                        type="button" 
                                        onClick={() => setEditField("name")} 
                                        >
                                    Edit
                                    </PrimaryButton>
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
                                    <PrimaryButton
                                        type="button"
                                        onClick={()=>saveField("email")}
                                    >
                                        Save
                                    </PrimaryButton>
                                ) :(
                                    <PrimaryButton
                                        type="button"
                                        onClick={() => setEditField("email")}
                                    >
                                        Edit
                                    </PrimaryButton>
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
                                    <PrimaryButton
                                        onClick={()=>saveField("phoneNumber")}>
                                        Save
                                    </PrimaryButton>
                                ) :(
                                    <PrimaryButton
                                        onClick={() => setEditField("phoneNumber")}>
                                        Edit
                                    </PrimaryButton>
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
                                        {clientStatusMap[client.status]}
                                    </span>
                                </span>
                            </div>
                            <div style={{
                                ...rowStyle, 
                                display: "flex", 
                                justifyContent: "center"
                            }}
                            >
                                <span style={labelStyle}>
                                    {
                                    client.status === 0 ? (
                                    <BlockButton onClick={() => updateClientStatus(1)}>
                                        Block
                                    </BlockButton>
                                    ) : (
                                    <UnblockButton onClick={() => updateClientStatus(0)}>
                                        Unblock
                                    </UnblockButton>)
                                    }
                                    
                                </span>
                            </div>
                            <div style={{
                                ...rowStyle, 
                                display: "flex", 
                                justifyContent: "center"
                            }}
                            >
                                <span style={labelStyle}>
                                    {
                                        client.status === 0 ? (
                                            <BlockButton 
                                                onClick={() => updateClientStatus(2)}
                                            >
                                                Suspend
                                            </BlockButton>
                                        ) : (
                                            <UnblockButton 
                                                onClick={() => updateClientStatus(0)}
                                            >
                                                Unblock
                                            </UnblockButton>
                                        )
                                    }
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </CardDetailsPage>
            <TableCard>
                <h1 style={{
                    textAlign: "center",
                    fontSize: "32px",
                    fontWeight: "400",
                    marginBottom: "40px"
                }}
                >
                    Accounts
                </h1>
                <table
                    cellPadding="5"
                    style={{ 
                        width: "100%", 
                        marginTop: "30px", 
                        justifyContent: "center"
                }}
                >
                    <thead>
                    <tr>
                        <TableColumn>ID</TableColumn>
                        <TableColumn>Balance</TableColumn>
                        <TableColumn>Client ID</TableColumn>
                        <TableColumn>Status</TableColumn>
                        <TableColumn>Account Plan</TableColumn>
                        <TableColumn>Account Type</TableColumn>
                        <TableColumn>Created date</TableColumn>
                    </tr>
                    </thead>
                    <tbody>
                    {account?.map(account => (
                        <tr key={account.id}>
                            <TableCell>
                                <Link to={`/accounts/${account.accountId}`} 
                                      className="client-link">
                                    {account.accountId}
                                </Link>
                            </TableCell>
                            <TableCell>{account.balance}</TableCell>
                            <TableCell>{account.clientId}</TableCell>
                            <TableCell>
                                    <span style={{
                                        color: account.status === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {accountStatusMap[account.status]}
                                    </span>
                            </TableCell>
                            <TableCell>
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
                            </TableCell>
                            <TableCell>
                                        <span style={{
                                            color: account.accountType === 0
                                                ? "#2563eb"
                                                : account.accountType === 1
                                                    ? "#22c55e"
                                                    : "#9333ea",
                                            fontWeight: "bold"
                                        }}>{accountType[account.accountType]}
                                        </span>
                            </TableCell>
                            <TableCell>
                                {new Date(account.createdAt).toLocaleString()}
                            </TableCell>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </TableCard>
        </div>
    )
}

export default ClientDetailsPage;