import RegisterEmployeeForm from "../../components/forms/employees/EmployeeForm.jsx";
import styles from "../../components/layout/PageLayout/layout.module.css";
function RegisterEmployeePage() {

    return (
        <div className={styles.pageHeader}>
            <RegisterEmployeeForm />
        </div>
    )
}

export default RegisterEmployeePage;