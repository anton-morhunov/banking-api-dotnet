import {Routes, Route} from 'react-router-dom'
import LoginPage from './pages/LoginPage.jsx'
import SearchingPage from "./pages/clients/SearchingPage.jsx";
import CreateClientPage from "./pages/clients/CreateClientPage.jsx";
import AccountsPage from "./pages/accounts/AccountsPage.jsx";
import EmployeesPage from "./pages/employees/EmployeesPage.jsx";
import ProtectedRoute from "./components/ProtectedRoute.jsx";
import RegisterEmployeePage from "./pages/employees/RegisterEmployeePage.jsx";
import CreateAccountPage from "./pages/accounts/CreateAccountPage.jsx";
import ClientDetailsPage from "./pages/clients/ClientDetailsPage.jsx";
import AccountDetailsPage from "./pages/accounts/AccountDetailsPage.jsx";

function App() {
    return(
        <Routes>
            <Route path="/" element={<LoginPage/>} />
            <Route path="/home" element={<ProtectedRoute> <SearchingPage/> </ProtectedRoute>} />
            <Route path="/create_client" element={<ProtectedRoute> <CreateClientPage/> </ProtectedRoute>} />
            <Route path="/colleagues" element={<ProtectedRoute><EmployeesPage/></ProtectedRoute>} />
            <Route path="/accounts" element={<ProtectedRoute><AccountsPage/></ProtectedRoute>} />
            <Route path="/colleagues" element={<ProtectedRoute><EmployeesPage/></ProtectedRoute>}/>
            <Route path="/register_user" element={<ProtectedRoute><RegisterEmployeePage/></ProtectedRoute>}/>
            <Route path="/create_account" element={<ProtectedRoute><CreateAccountPage/></ProtectedRoute>}/>
            <Route path="/clients/:id" element={<ProtectedRoute><ClientDetailsPage/></ProtectedRoute>}/>
            <Route path="/accounts/:id" element={<ProtectedRoute><AccountDetailsPage/></ProtectedRoute>}/>
        </Routes>
    )
}

export default App;
