import axios from 'axios';

export const ACTION_TYPE = {
    SET_ACTION: 'SET_ACTION',
    GET_STOCK_SUCCESS: 'GET_STOCK_SUCCESS',
    GET_STOCK_REQUEST: 'GET_STOCK_REQUEST',
    GET_STOCK_FAILURE: 'GET_STOCK_FAILURE',

    LIST_STOCK_SUCCESS: 'LIST_STOCK_SUCCESS',
    LIST_STOCK_REQUEST: 'LIST_STOCK_REQUEST',
    LIST_STOCK_FAILURE: 'LIST_STOCK_FAILURE',

    POST_NEWORDER_SUCCESS: 'POST_NEWORDER_SUCCESS',
    POST_NEWORDER_REQUEST: 'POST_NEWORDER_REQUEST',
    POST_NEWORDER_FAILURE: 'POST_NEWORDER_FAILURE',

    GET_ORDER_SUCCESS: 'GET_ORDER_SUCCESS',
    GET_ORDER_REQUEST: 'GET_ORDER_REQUEST',
    GET_ORDER_FAILURE: 'GET_ORDER_FAILURE',
};

const API_URL = 'http://localhost:5225';
const getRequest = (route) => axios.get(`${API_URL}/${route}`);
const postRequest = (route, payload) => axios.post(`${API_URL}/${route}`, payload);
const formRequest = (route, fields) => {
    const body = new FormData();
    for (let [key, value] of Object.entries(fields))
        body.append(key, value);

    return axios({
        method: 'post',
        url: `${API_URL}/${route}`,
        data: body,
        headers: { "Content-Type": "multipart/form-data" }
    });
}

export const sendContactMessage = (fields) => dispatch => {
    dispatch({ type: ACTION_TYPE.SEND_MESSAGE_REQUEST });
    return formRequest(`send-message`, fields)
        .then(() => {
            dispatch({
                type: ACTION_TYPE.SEND_MESSAGE_SUCCESS
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.SEND_MESSAGE_FAILURE,
                payload: response?.data
            });
        })
}

export const getAllStock = dispatch => {
    dispatch({ type: ACTION_TYPE.LIST_STOCK_REQUEST });
    return getRequest('stock')
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.LIST_STOCK_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.LIST_STOCK_FAILURE,
                payload: response?.data
            });
        });
};

export const getStock = (slug) => dispatch => {
    dispatch({ type: ACTION_TYPE.GET_STOCK_REQUEST });
    return getRequest(`stock/slug/${slug}`)
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.GET_STOCK_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.GET_STOCK_FAILURE,
                payload: response?.data
            });
        });
};

export const sendNewOrder = (values) => dispatch => {
    dispatch({ type: ACTION_TYPE.POST_NEWORDER_REQUEST });
    return postRequest(`orders`, values)
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.POST_NEWORDER_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.POST_NEWORDER_FAILURE,
                payload: response?.data
            });
        });
};

export const resetNewOrderResponse = dispatch => dispatch({ type: ACTION_TYPE.POST_NEWORDER_REQUEST });

export const getCustomerOrder = (accessKey) => dispatch => {
    dispatch({ type: ACTION_TYPE.GET_ORDER_REQUEST });
    return getRequest(`orders/customer/${accessKey}`)
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.GET_ORDER_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.GET_ORDER_FAILURE,
                payload: response?.data
            });
        });
};

/*

export const getCategories = dispatch => {
    dispatch({ type: ACTION_TYPE.LIST_CATEGORIES_REQUEST });
    return getRequest('category')
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.LIST_CATEGORIES_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.LIST_CATEGORIES_FAILURE,
                payload: response?.data
            });
        })
}

export const getSearchResult = (query) => dispatch => {
    dispatch({ type: ACTION_TYPE.FETCH_SEARCH_REQUEST });
    return getRequest(`search/query/${query}`)
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.FETCH_SEARCH_SUCCESS,
                payload: data
            });
        })
        .catch((err) => {
            dispatch({
                type: ACTION_TYPE.FETCH_SEARCH_FAILURE,
                payload: err
            });
        })
}*/