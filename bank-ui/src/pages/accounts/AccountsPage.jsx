import {api} from "../../api/api.js";
import {useState, useEffect} from "react";
import {useNavigate, Link} from "react-router-dom";

function AccountsPage() {
    const navigate = useNavigate();

    const [inputId, setId] = useState("")
    const [account, setAccount] = useState(null)

    const [accounts, setAccounts] = useState([])

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

    return (
        <div style={{ padding: "20px" }}>

            <h1 style={{
                textAlign: "left",
                fontSize: "32px",
                fontWeight: "400",
                marginBottom: "20px"}}>
                Accounts information
            </h1>

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

                        <button
                            className="primary-btn"
                            onClick={findAccount}
                        >
                            Find
                        </button>

                        <button className="primary-btn"
                                onClick={() => navigate("/create_account")}>
                            Create New
                        </button>
                    </div>
                    <div style={styles.card} >
                        <table
                            cellPadding="5"
                            style={{ width: "100%", marginTop: "30px" }}
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
                            {(account ? [account] : accounts).map(account => (
                                <tr key={account.id}>
                                    <td style={styles.td}>
                                        <Link to={`/accounts/${account.accountId}`} className="client-link">{account.accountId}
                                        </Link>
                                    </td>
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
                                            color: account.plan === 0 
                                                ? "#64748b" 
                                                : account.plan === 1 
                                                    ? "#eab308" 
                                                    : "#0f766e",
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

                <div style={{
                    width: "250px",
                    marginLeft: "20px",
                    boxShadow: "-4px 0 10px rgba(0, 0, 0, 0.08)",
                    borderLeft: "1px solid rgba(0,0,0,0.08)",
                    padding: "20px"}}
                     className="sidebar">

                    <h3 className="sidebar-title">Panel</h3>

                    <div
                        className="sidebar-item"
                        onClick={() => navigate("/accounts")}
                    >
                        Accounts
                    </div>

                    <div
                        className="sidebar-item"
                        onClick={() => navigate("/home")}
                    >
                        Clients
                    </div>

                    <div className="sidebar-item"
                         onClick={() => navigate("/colleagues")}>
                        Colleagues
                    </div>
                </div>

            </div>
        </div>
    );
}

export default AccountsPage;