import {Routes, Route} from 'react-router-dom'
import LoginPage from './pages/LoginPage.jsx'
import SearchingPage from "./pages/SearchingPage.jsx";
import CreateClientPage from "./pages/CreateClientPage.jsx";
import AccountsPage from "./pages/AccountsPage.jsx";
import EmployeesPage from "./pages/EmployeesPage.jsx";

function App() {
    return(
        <Routes>
            <Route path="/" element={<LoginPage/>} />
            <Route path="/home" element={<SearchingPage/>} />
            <Route path="/create_client" element={<CreateClientPage/>} />
            <Route path="/colleagues" element={<EmployeesPage/>} />
            <Route path="/accounts" element={<AccountsPage/>} />
        </Routes>
    )
}

export default App;
