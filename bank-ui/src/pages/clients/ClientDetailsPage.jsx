import {useParams, Link} from "react-router-dom";
import {useState, useEffect} from "react";
import BlockButton from "../../components/ui/Button/BlockButton.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import CardDetailsPage from "../../components/ui/Card/CardDetailsPage.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import TableCell from "../../components/ui/Card/TableCell.jsx";
import { accountPlan, accountType, accountStatusMap} from "../../constants/accountConstants.js"
import { clientStatusMap } from "../../constants/clientConstants.js"
import UnblockButton from "../../components/ui/Button/UnblockButton.jsx";
import InnerCard from "../../components/ui/Card/InnerCard.jsx";
import {
    rowStyleText,
    labelStyle,
    valueStyle,
    sectionStyle
} from "../../styles/clientDetailsStyles";
import {
    getClientById,
    updateClient,
    updateClientStatusRequest
} from "../../services/clientService.js";
import {
    getAccountsByClientId
} from "../../services/accountService.js";
import EditableClientField from "../../components/forms/clients/EditableClientsField.jsx";
import {descriptionStyle, headerStyle} from "../../styles/detailsPageGeneralStyle.js";

function ClientDetailsPage(){
    
    const { id } = useParams();
    const [client, setClient] = useState(null);
    const [account, setAccounts] = useState([]);
    const [editingField, setEditField] = useState(null);
    const [editedClient, setEditedClient] = useState({});
    
    useEffect(()=> {
        const fetchClient = async () => {

            try {
                const response = await getClientById(id);
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
             await updateClient(
                client.id, 
                editedClient
            );
            setClient(editedClient);

            setEditField(null);

        } catch (err) {
            console.log(err.response);
        }
    };
    useEffect(() => {
        const fetchAccounts = async () => {
            try{
                const response = await getAccountsByClientId(id);

                setAccounts(response.data);
            }catch(error){
                console.log(error);
            }
        }; 
        fetchAccounts();
    }, [id])
    
    const updateClientStatus = async (status) => {
        try{
            
            const response = await updateClientStatusRequest(
                client.id, 
                status
            );

            console.log(response.data);
            
            setClient({
                ...client,
                status
            })
        } catch(err){
            console.log(err.response);
        }
    }
    
    if(!client) return <div className="loader"></div>;
    
    return(

        <div style={{ padding: "30px" }}>
            <CardDetailsPage>
            <h1 style={headerStyle}>
                {client.id}
            </h1>
            <h2 style={descriptionStyle}
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
                        <InnerCard>
                            <div style={sectionStyle}>

                                <EditableClientField
                                    label="Name"
                                    field="name"
                                    editingField={editingField}
                                    editedClient={editedClient}
                                    setEditedClient={setEditedClient}
                                    setEditField={setEditField}
                                    saveField={saveField}
                                />

                                <EditableClientField
                                    label="Email"
                                    field="email"
                                    editingField={editingField}
                                    editedClient={editedClient}
                                    setEditedClient={setEditedClient}
                                    setEditField={setEditField}
                                    saveField={saveField}
                                />

                                <EditableClientField
                                    label="Phone Number"
                                    field="phoneNumber"
                                    editingField={editingField}
                                    editedClient={editedClient}
                                    setEditedClient={setEditedClient}
                                    setEditField={setEditField}
                                    saveField={saveField}
                                />

                            </div>
                        </InnerCard>
                        <InnerCard>
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "column",
                                    justifyContent: "space-between",
                                    height: "100%"
                                }}
                            >
                                <div>
                                    <div style={rowStyleText}>
                                        <span style={labelStyle}>Created At</span>

                                        <span style={valueStyle}>
                                            {new Date(client.created).toLocaleString()}
                                        </span>
                                    </div>
                                    <div style={rowStyleText}>
                                        <span style={labelStyle}>Status</span>
                                        <span
                                            style={{
                                                color: client.status === 0 ? "green" : "red",
                                                fontWeight: "bold"
                                            }}
                                        >
                                            {clientStatusMap[client.status]}
                                        </span>
                                    </div>
                                </div>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "column",
                                        alignItems: "center"
                                    }}
                                >
                                    <h3
                                        style={{
                                            marginBottom: "24px",
                                            color: "#94a3b8",
                                            fontSize: "28px"
                                        }}
                                    >
                                        Actions
                                    </h3>
                                    <div
                                        style={{
                                            display: "flex",
                                            gap: "32px",
                                            justifyContent: "center"
                                        }}
                                    >
                                        {
                                            client.status === 0 ? (
                                                <>
                                                    <BlockButton
                                                        onClick={() => updateClientStatus(1)}
                                                    >
                                                        Block
                                                    </BlockButton>

                                                    <BlockButton
                                                        onClick={() => updateClientStatus(2)}
                                                    >
                                                        Suspend
                                                    </BlockButton>
                                                </>
                                            ) : (
                                                <UnblockButton
                                                    onClick={() => updateClientStatus(0)}
                                                >
                                                    Unblock
                                                </UnblockButton>
                                            )
                                        }
                                    </div>
                                </div>
                            </div>
                        </InnerCard>
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
            <TableCard>
                <h1 style={{
                    textAlign: "center",
                    fontSize: "32px",
                    fontWeight: "400",
                    marginBottom: "40px"
                }}
                >
                    Notes
                </h1>
            </TableCard>
        </div>
    )
}
export default ClientDetailsPage;