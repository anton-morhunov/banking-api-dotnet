import {api} from "../../api/api.js";
import {useState, useEffect} from "react";
import {useNavigate, Link} from "react-router-dom";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import TableCell from "../../components/ui/Card/TableCell.jsx";
import PageTitle from "../../components/ui/Typography/PageTitle.jsx";
import Sidebar from "../../components/layout/Sidebar/Sidebar.jsx";

function AccountsPage() {
    const navigate = useNavigate();

    const [inputId, setId] = useState("")
    const [account, setAccount] = useState(null)

    const [accounts, setAccounts] = useState([])

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

    useEffect(() => {
        api.get("/accounts")
            .then(response => setAccounts(response.data))
            .catch(error => console.log(error));
    }, []);

    const findAccount = async () => {
        if(inputId.trim() === "") return;

        try {
            const res = await api.get(`/accounts?accountId=${inputId}`);
            setAccount(res.data);
        } catch (error) {
            console.log(error);
        }
    }
    
    if(!accounts) return <div className="loader"></div>;

    return (
        <div style={{ padding: "20px" }}>

            <PageTitle>
                Accounts information
            </PageTitle>
            <div style={{ display: "flex", alignItems: "flex-start" }}>
                <div style={{ flex: 1 }}>
                    <div style={{
                        display: "flex",
                        gap: "10px",
                        marginBottom: "20px",
                    }}>
                        <input
                            className="search-input"
                            style={{ width: "70%", padding: "8px" }}
                            value={inputId}
                            onChange={e => setId(e.target.value)}
                            placeholder="Enter Account ID"
                        />
                        <PrimaryButton
                            onClick={findAccount}
                        >
                            Find
                        </PrimaryButton>
                        <PrimaryButton
                                onClick={() => navigate("/create_account")}>
                            Create New
                        </PrimaryButton>
                    </div>
                    <TableCard>
                        <table
                            cellPadding="5"
                            style={{ width: "100%", marginTop: "30px" }}
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
                            {(account ? [account] : accounts).map(account => (
                                <tr key={account.id}>
                                    <TableCell>
                                        <Link to={`/accounts/${account.accountId}`} className="client-link">{account.accountId}
                                        </Link>
                                    </TableCell>
                                    <TableCell>{account.balance}</TableCell>
                                    <TableCell>{account.clientId}</TableCell>
                                    <TableCell>
                                    <span style={{
                                        color: account.status === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {statusMap[account.status]}
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
                <Sidebar />
            </div>
        </div>
    );
}

export default AccountsPage;