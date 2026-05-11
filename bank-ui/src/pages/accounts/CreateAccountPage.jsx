import AccountForm from "../../components/accounts/AccountForm.jsx";
function CreateAccountPage() {

    return (
        <div style={{
            minHeight: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center"
        }}>
            <AccountForm />
        </div>
    )
}

export default CreateAccountPage;