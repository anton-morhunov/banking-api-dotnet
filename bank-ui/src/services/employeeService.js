import {api} from "../api/api.js";

export const getEmployeeById = async (id) => {
    
    return await api.get(`/users/${id}`);
}