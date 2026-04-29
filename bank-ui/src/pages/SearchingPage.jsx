import {useEffect, useState} from "react";
import {api} from "../api/api"

function SearchingPage(){
    
    const [inputId, setId] = useState("")
    const [client, setClient] = useState(null)
    
    const findClient = async () => {
        if(inputId.trim() === "") return;

        try {
            const res = await api.get(`/clients/${inputId}`);
            setClient(res.data);
        } catch (error) {
            console.log(error);
        }
    }
    
    return(
        <div style={{padding : "20px"}}>
            <h1>Find Client</h1>
            <input 
                value={inputId} 
                onChange={e => setId(e.target.value)} 
                placeholder="Enter Client ID" />
            
            <button onClick={findClient}>Find</button>
            { client && (
            <ul>
                <li>
                    {client.name} - {client.email}
                </li>
            </ul>
            )}
        </div>
    );
}

export default SearchingPage;