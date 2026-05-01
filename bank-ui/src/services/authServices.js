import { api } from '../api/api';

export const login = async (login, password) => {
    const res = await api.post("auth/login", 
        {email: login, 
            passwordHash: password
        });
    
    localStorage.setItem("token", res.data.token);
    
    return res.data;
}

export const logout = () => {
    localStorage.removeItem("token");
}

export const getToken = () => {
    return localStorage.getItem("token");   
}