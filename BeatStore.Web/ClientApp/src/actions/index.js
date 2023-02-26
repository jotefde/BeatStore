import axios from 'axios';
import {useMutation, useQuery} from "@tanstack/react-query";
import { API_URL } from "Constants";

export const ACTION_TYPE = {
    GET_STOCK_REQUEST: 'GET_STOCK_REQUEST',
    LIST_STOCK_REQUEST: 'LIST_STOCK_REQUEST',
    POST_NEW_ORDER_REQUEST: 'POST_NEW_ORDER_REQUEST',
    GET_ORDER_REQUEST: 'GET_ORDER_REQUEST',
};

const getRequest = (route) => axios
    .get(`${API_URL}/${route}`)
    .then(({data}) => data)
    .catch(({response}) => {
        return Promise.reject(response.data.errors);
    });

const postRequest = (route, payload) => axios
    .post(`${API_URL}/${route}`, payload)
    .then(({data}) => data)
    .catch(({response}) => {
        return Promise.reject(response.data.errors);
    });

const formRequest = (route, fields) => {
    const body = new FormData();
    for (let [key, value] of Object.entries(fields))
        body.append(key, value);

    return axios({
        method: 'post',
        url: `${API_URL}/${route}`,
        data: body,
        headers: { "Content-Type": "multipart/form-data" }})
        .then(({data}) => data)
        .catch(({response}) => {
            return Promise.reject(response.data);
        });
}

const retry = 1;

export const useGetSingleStock = (trackSlug) => {
    return useQuery({
        queryKey: [ACTION_TYPE.GET_STOCK_REQUEST,trackSlug],
        queryFn: () => getRequest(`stock/slug/${trackSlug}`),
        retry
    })
};

export const useListStock = () => {
    return useQuery({
        queryKey: [ACTION_TYPE.LIST_STOCK_REQUEST],
        queryFn: () => getRequest(`stock`),
        retry
    })
};

export const usePostNewOrder = () => {
    return useMutation({
        mutationFn: values => postRequest(`orders`, values),
        retry
    })
};

export const useGetCustomerOrder = (accessKey) => {
    return useQuery({
        queryKey: [ACTION_TYPE.GET_ORDER_REQUEST, accessKey],
        queryFn: () => getRequest(`orders/customer/${accessKey}`),
        retry
    })
};

export const usePostContactMessage = () => {
    return useMutation({
        mutationFn: form => postRequest(`orders`, form),
        retry
    })
};