import AccountForm from "../../components/forms/accounts/AccountForm.jsx";
import styles from "../../components/layout/PageLayout/layout.module.css";
function CreateAccountPage() {

    return (
        <div className={styles.pageHeader}>
            <AccountForm />
        </div>
    )
}

export default CreateAccountPage;