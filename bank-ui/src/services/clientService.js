import {api} from "../api/api";

export const getClientById = async (id) => {

    return await api.get(`/clients/${id}`);
}
export const updateClient = async (id, dto) => {
    return await api.put(`/clients/${id}`, dto);
};

export const updateClientStatusRequest = async (id, status) => {
    return await api.patch(`/clients/${id}?dto=${status}`);
}