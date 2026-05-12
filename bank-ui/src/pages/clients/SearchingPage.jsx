import {useEffect, useState} from "react";
import {api} from "../../api/api";
import {useNavigate, Link} from "react-router-dom";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import TableCell from "../../components/ui/Card/TableCell.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import PageTitle from "../../components/ui/Typography/PageTitle.jsx";
import Sidebar from "../../components/layout/Sidebar/Sidebar.jsx";
import Input from "../../components/ui/Input/Input.jsx";
import styles from "../../components/layout/PageLayout/layout.module.css";
import { clientStatusMap } from "../../constants/clientConstants.js"

function SearchingPage(){
    
    const navigate = useNavigate();
    
    const [inputId, setId] = useState("")
    const [client, setClient] = useState(null)
    const [clients, setClients] = useState([])
    
    useEffect(() => {
        api.get("/clients")
            .then(response => setClients(response.data))
            .catch(error => console.log(error));
    }, []);
    
    const findClient = async () => {
        if(inputId.trim() === "") return;

        try {
            const res = await api.get(`/clients/${inputId}`);
            setClient(res.data);
        } catch (error) {
            console.log(error);
        }
    }

    if(!clients) return <div className="loader"></div>;

    return (
        <div className={styles.pageContainer}>
            <PageTitle> 
                Clients information
            </PageTitle>
            <div className={styles.pageLayout}>
                <div className={styles.pageContent}>
                    <div className={styles.actionBar}>
                        <Input
                            value={inputId}
                            onChange={e => setId(e.target.value)}
                            placeholder="Enter Client ID"
                        />
                        <PrimaryButton
                            onClick={findClient}
                        >
                            Find
                        </PrimaryButton>
                        <PrimaryButton 
                                onClick={() => navigate("/create_client")}>
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
                            <TableColumn>Name</TableColumn>
                            <TableColumn>Email</TableColumn>
                            <TableColumn>Phone number</TableColumn>
                            <TableColumn>Status</TableColumn>
                            <TableColumn>Created date</TableColumn>
                        </tr>
                        </thead>
                        <tbody>
                        {(client ? [client] : clients).map(client => (
                            <tr key={client.id}>
                                <TableCell>{client.id}</TableCell>
                                <TableCell>{client.name}</TableCell>
                                <TableCell>
                                    <Link to={`/clients/${client.id}`} className="client-link">
                                        {client.email}
                                    </Link>
                                </TableCell>
                                <TableCell>{client.phoneNumber}</TableCell>
                                <TableCell>
                                    <span style={{
                                        color: client.status === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {clientStatusMap[client.status]}
                                    </span>
                                </TableCell>
                                <TableCell>
                                    {new Date(client.created).toLocaleString()}
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

export default SearchingPage;