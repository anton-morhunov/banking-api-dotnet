import {api} from "../api/api";

export const getAccountsByClientId = async (id) => {
    return await api.get(`/accounts/client/${id}`);
}
export const getAccountById = async (id) => {
    return await api.get(`/accounts/${id}`);
}

export const updateAccountPlan = async (id, plan) => {
    return await api.patch(`/accounts/${id}/plan`, {plan});
}

export const updateAccountStatus = async (id, status) => {
    return await api.patch(`/accounts/${id}/status`, {status});
}