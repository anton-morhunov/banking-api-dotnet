import {useEffect, useState} from 'react'
import {api} from "../api/api"

function HomePage() {

    const[data, setData] = useState([])

    useEffect(() => {
        api.get("/clients")
            .then(res => setData(res.data))
            .catch(err => console.log(err))
    }, []);

    return(
        <div>
            <h1>Clients</h1>
            <ul>
                {data.map(client => (
                    <li key={client.id}>
                        {client.name} - {client.email}
                    </li>
                ))}
            </ul>
        </div>
    )
}
export default HomePage;