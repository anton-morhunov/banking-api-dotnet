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

export const GetCommentsByClientId = async (clientId) => {
    return await api.get(`/comments/client/${clientId}`);
}

export const CreateCommitAsync = async ({text, clientId, userId}) => {
    return await api.post(`/comments`, {
        text, 
        clientId, 
        userId
    });
}
export const DeleteCommitAsync = async (commentId) => {
    return await api.delete(`/comments`, {params: {commentId}});
}