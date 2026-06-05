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
    CreateCommitAsync, DeleteCommitAsync,
    getClientById,
    GetCommentsByClientId,
    updateClient,
    updateClientStatusRequest
} from "../../services/clientService.js";
import {
    getAccountsByClientId
} from "../../services/accountService.js";
import EditableClientField from "../../components/forms/clients/EditableClientsField.jsx";
import {descriptionStyle, headerStyle} from "../../styles/detailsPageGeneralStyle.js";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import {
    modalButtons,
    modalOverlay,
    modal,
    modalHeaderStyle,
    modalTextStyle,
    modalInputStyle, modalCancelButtonStyle, modalConfirmButtonStyle
} from "../../styles/commentModalStyle.js";
import {createPortal} from "react-dom";
import { Trash2 } from "lucide-react";
import { Pencil } from "lucide-react";

function ClientDetailsPage(){
    
    const { id } = useParams();
    const [client, setClient] = useState(null);
    const [account, setAccounts] = useState([]);
    const [editingField, setEditField] = useState(null);
    const [editedClient, setEditedClient] = useState({});
    const [comments, setComments] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [commentText, setCommentText] = useState('');
    const [userId, setUserId] = useState('');
    const [deleteComments, setDeleteComments] = useState({});
    
    
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

    useEffect(() => {
        const fetchComments = async () => {
            try{
                const response = await GetCommentsByClientId(id);

                setComments(response.data);
            }catch(error){
                console.log(error);
            }
        }
        fetchComments();
    }, [id]);

    const handleSubmit = async () => {
        try {
            await CreateCommitAsync({ 
                text : commentText, 
                clientId : Number(id), 
                userId : Number(userId)}
            );
            setIsModalOpen(false);
            setCommentText('');
            setUserId('');
            
            const response = await GetCommentsByClientId(id);
            setComments(response.data);
        } catch (error) {
            console.log(error);
        }
    };
    
    const deleteComment = async (commentId) => {
        try{
            await DeleteCommitAsync(commentId);
            setComments(prev => prev.filter(c => c.commentId !== commentId));
        }catch(error){
            console.log(error);
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
                            <TableCell>
                                {new Intl.NumberFormat('en-US', 
                                    {style: 'currency', 
                                        currency: 'USD'
                                    }).format(account.balance)}
                            </TableCell>
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
                        Comments
                    </h1>
                    <PrimaryButton 
                        onClick={() => 
                            setIsModalOpen(true)}
                    >
                        + New Comment
                    </PrimaryButton>
                {isModalOpen && createPortal(
                    <div 
                        onClick={() => 
                            setIsModalOpen(false)}
                        style={modalOverlay}
                    >
                        <div
                            onClick={(e) => 
                                e.stopPropagation()}
                            style={modal}>
                            <h3 
                                style={modalHeaderStyle}
                            >
                                New Comment
                            </h3>
                            <textarea
                                placeholder="Enter comment..."
                                value={commentText}
                                onChange={(e) => 
                                    setCommentText(e.target.value)}
                                style={modalTextStyle}
                            />

                            <input
                                placeholder="Enter userId..."
                                value={userId}
                                onChange={(e) => 
                                    setUserId(e.target.value)}
                                style={modalInputStyle}
                            />

                            <div 
                                style={modalCancelButtonStyle}>
                                <button 
                                    onClick={() => 
                                        setIsModalOpen(false)}
                                >
                                    Cancel
                                </button>
                                <button 
                                    onClick={handleSubmit} 
                                    style={modalConfirmButtonStyle}
                                >
                                    Save
                                </button>
                            </div>
                        </div>
                    </div>,
                    document.body
                )}
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
                        <TableColumn>CommentId</TableColumn>
                        <TableColumn>Comment</TableColumn>
                        <TableColumn>User</TableColumn>
                        <TableColumn>Created date</TableColumn>
                        <TableColumn>Edit</TableColumn>
                        <TableColumn>Delete</TableColumn>
                    </tr>
                    </thead>
                    <tbody>
                    {comments?.map(comments => (
                        <tr key={comments.clientId}>
                            <TableCell>{comments.commentId}</TableCell>
                            <TableCell>{comments.text}</TableCell>
                            <TableCell>{comments.userId}</TableCell>
                            <TableCell>
                                {new Date(comments.createdAt).toLocaleString()}
                            </TableCell>
                            <TableCell>
                                <button style={{
                                    background: '#1a73e8',
                                    border: 'none',
                                    cursor: 'pointer',
                                    color: '#1a73e8',
                                    fontSize: '16px',
                                    padding: '4px 8px',
                                    borderRadius: '6px',
                                    transition: 'background 0.2s',
                                }}>
                                    <Pencil size={20} style={{color: '#ffffff'}}/>
                                </button>
                            </TableCell>
                            <TableCell>
                                <button
                                    onClick={() => deleteComment(comments.commentId)}
                                    style={{
                                        background: 'red',
                                        border: 'none',
                                        cursor: 'pointer',
                                        color: '#e81a1a',
                                        fontSize: '16px',
                                        padding: '4px 8px',
                                        borderRadius: '6px',
                                        transition: 'background 0.2s',
                                    }}
                                >
                                    <Trash2 size={20} style={{color: '#ffffff'}}/>
                                </button>
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