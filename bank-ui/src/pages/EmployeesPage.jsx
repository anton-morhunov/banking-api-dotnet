import {useEffect, useState} from "react";
import {api} from "../api/api";
import {useNavigate} from "react-router-dom";
function EmployeesPage() {

    const navigate = useNavigate();

    const [inputId, setId] = useState("")
    const [employee, setEmployee] = useState(null)

    const [employees, setEmployees] = useState([])

    const statusMap = {
        0: "Administrator",
        1: "Employee",
    }

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

    return (
        <div style={{ padding: "20px" }}>

            <h1 style={{
                textAlign: "left",
                fontSize: "32px",
                fontWeight: "400",
                marginBottom: "20px"}}>
                Employees information
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
                            placeholder="Enter Client ID"
                        />

                        <button
                            className="primary-btn"
                            onClick={findEmployee}
                        >
                            Find
                        </button>

                        <button
                            className="primary-btn"
                            onClick={() => navigate("/register_user")}
                        >
                            Register new employee
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
                                <th style={styles.th}>Email</th>
                                <th style={styles.th}>UserRole</th>
                            </tr>
                            </thead>

                            <tbody>
                            {(employee ? [employee] : employees).map(employee => (
                                <tr key={employee.id}>
                                    <td style={styles.td}>{employee.id}</td>
                                    <td style={styles.td}>{employee.email}</td>
                                    <td style={styles.td}>
                                    <span style={{
                                        color: employee.userRole === 0 ? "green" : "red",
                                        fontWeight: "bold"
                                    }}>
                                        {statusMap[employee.userRole]}
                                    </span>
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

export default EmployeesPage;