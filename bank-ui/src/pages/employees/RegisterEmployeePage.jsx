import RegisterEmployeeForm from "../../components/employees/EmployeeForm.jsx";
function RegisterEmployeePage() {

    return (
        <div
            style={{
                padding: "2px",
                minHeight: "100vh",
                display: "flex",
                justifyContent: "center",
                alignItems: "center" }}
        >
            <RegisterEmployeeForm />
        </div>
    )
}

export default RegisterEmployeePage;