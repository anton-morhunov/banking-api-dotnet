import {Routes, Route} from 'react-router-dom'
import LoginPage from './pages/LoginPage.jsx'
import HomePage from './pages/HomePage.jsx'
import SearchingPage from "./pages/SearchingPage.jsx";

function App() {
    return(
        <Routes>
            <Route path="/" element={<LoginPage/>} />
            <Route path="/home" element={<SearchingPage/>} />
        </Routes>
    )
}

export default App;
