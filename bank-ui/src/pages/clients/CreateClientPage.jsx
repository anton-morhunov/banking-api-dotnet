import ClientForm from "../../components/forms/clients/ClientForm.jsx";
function CreateClient() {
    
    return (
        <div style={{
            minHeight: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center"
        }}>
            <ClientForm />
        </div>
    )
}

export default CreateClient;