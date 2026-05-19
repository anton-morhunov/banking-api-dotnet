import LoginForm from "../../components/forms/login/LoginForm.jsx";
import styles from "../../components/layout/PageLayout/layout.module.css";

function LoginPage() {

    return (
        <div className={styles.loginFormContainer}>
            <LoginForm />
        </div>
    )
}

export default LoginPage;