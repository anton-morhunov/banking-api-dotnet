import {useNavigate} from 'react-router-dom'

function LoginPage() {
    
    const navigate = useNavigate();

    return(
        <>
            <h1>Bank API Back Office</h1>
            <button onClick={() => navigate("/home")}>
                Go Home
            </button>
        </>
    );
}

export default LoginPage;