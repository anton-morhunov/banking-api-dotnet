import {Routes, Route} from 'react-router-dom'
import LoginPage from './pages/LoginPage.jsx'
import SearchingPage from "./pages/SearchingPage.jsx";
import CreateClientPage from "./pages/CreateClientPage.jsx";
import AccountsPage from "./pages/AccountsPage.jsx";
import EmployeesPage from "./pages/EmployeesPage.jsx";
import ProtectedRoute from "./components/ProtectedRoute.jsx";
import RegisterEmployeePage from "./pages/RegisterEmployeePage.jsx";
import CreateAccountPage from "./pages/CreateAccountPage.jsx";
import ClientDetailsPage from "./pages/ClientDetailsPage.jsx";

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
        </Routes>
    )
}

export default App;
