import axios from 'axios';

export const ACTION_TYPE = {
    LIST_TRACKS_SUCCESS: 'LIST_TRACKS_SUCCESS',
    LIST_TRACKS_REQUEST: 'LIST_TRACKS_REQUEST',
    LIST_TRACKS_FAILURE: 'LIST_TRACKS_FAILURE',

    LIST_CATEGORIES_SUCCESS: 'LIST_CATEGORIES_SUCCESS',
    LIST_CATEGORIES_REQUEST: 'LIST_CATEGORIES_REQUEST',
    LIST_CATEGORIES_FAILURE: 'LIST_CATEGORIES_FAILURE',

    SEND_MESSAGE_REQUEST: 'SEND_MESSAGE_REQUEST',
    SEND_MESSAGE_SUCCESS: 'SEND_MESSAGE_SUCCESS',
    SEND_MESSAGE_FAILURE: 'SEND_MESSAGE_FAILURE',

    FETCH_SEARCH_REQUEST: 'FETCH_SEARCH_REQUEST',
    FETCH_SEARCH_SUCCESS: 'FETCH_SEARCH_SUCCESS',
    FETCH_SEARCH_FAILURE: 'FETCH_SEARCH_FAILURE'
};

//https://api.pronatura.com.pl
const API_URL = 'https://api.pronatura.com.pl';
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

export const getTracks = dispatch => {
    dispatch({ type: ACTION_TYPE.LIST_TRACKS_REQUEST });
    return getRequest('track')
        .then(({ data }) => {
            dispatch({
                type: ACTION_TYPE.LIST_TRACKS_SUCCESS,
                payload: data
            });
        })
        .catch(({ response }) => {
            dispatch({
                type: ACTION_TYPE.LIST_TRACKS_FAILURE,
                payload: response?.data
            });
        });
};

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