import {useEffect, useState} from "react";
import {api} from "../../api/api";
import {useNavigate} from "react-router-dom";
import PrimaryButton from "../../components/ui/Button/Button.jsx";
import TableCard from "../../components/ui/Card/Card.jsx";
import TableCell from "../../components/ui/Card/TableCell.jsx";
import TableColumn from "../../components/ui/Card/TableColumn.jsx";
import PageTitle from "../../components/ui/Typography/PageTitle.jsx";
import Sidebar from "../../components/layout/Sidebar/Sidebar.jsx";
function EmployeesPage() {

    const navigate = useNavigate();
    const [inputId, setId] = useState("")
    const [employee, setEmployee] = useState(null)
    const [employees, setEmployees] = useState([])

    const statusMap = {
        0: "Administrator",
        1: "Employee",
    }
    
    useEffect(() => {
        api.get("/users")
            .then(response => setEmployees(response.data))
            .catch(error => console.log(error));
    }, []);

    const findEmployee = async () => {
        if(inputId.trim() === "") return;

        try {
            const res = await api.get(`/users/${inputId}`);
            setEmployee(res.data);
        } catch (error) {
            console.log(error);
        }
    }

    if(!employees) return <div className="loader"></div>;

    return (
        <div style={{ padding: "20px" }}>
            <PageTitle>
                Employees information
            </PageTitle>
            <div style={{ display: "flex", alignItems: "flex-start" }}>
                <div style={{ flex: 1 }}>
                    <div style={{
                        display: "flex",
                        gap: "10px",
                        marginBottom: "20px",
                    }}
                    >
                        <input
                            className="search-input"
                            style={{ width: "70%", padding: "8px" }}
                            value={inputId}
                            onChange={e => setId(e.target.value)}
                            placeholder="Enter Client ID"
                        />
                        <PrimaryButton
                            onClick={findEmployee}
                        >
                            Find
                        </PrimaryButton>
                        <PrimaryButton
                            onClick={() => navigate("/register_user")}
                        >
                            Register new employee
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
                                <TableColumn>Email</TableColumn>
                                <TableColumn>UserRole</TableColumn>
                            </tr>
                            </thead>
                            <tbody>
                            {(employee ? [employee] : employees).map(employee => (
                                <tr key={employee.id}>
                                    <TableCell>{employee.id}</TableCell>
                                    <TableCell>{employee.email}</TableCell>
                                    <TableCell>
                                    <span style={{
                                        color: employee.userRole === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {statusMap[employee.userRole]}
                                    </span>
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

export default EmployeesPage;