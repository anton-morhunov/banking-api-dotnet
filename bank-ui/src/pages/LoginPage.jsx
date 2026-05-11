import LoginForm from "../components/LoginForm.jsx";
function LoginPage() {

    return (
        <div style={{
            minHeight: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center"
        }}>
            <LoginForm />
        </div>
    )
}

export default LoginPage;