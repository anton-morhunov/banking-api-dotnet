import ClientForm from "../../components/forms/clients/ClientForm.jsx";
import styles from "../../components/layout/PageLayout/layout.module.css";
function CreateClient() {
    
    return (
        <div className={styles.pageHeader}>
            <ClientForm />
        </div>
    )
}

export default CreateClient;