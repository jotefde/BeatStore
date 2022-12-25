import { ACTION_TYPE } from 'actions';

const initState = {
    getAllStockResponse: {},
    getStockResponse: {},
    postNewOrderResponse: {},
};

const rootReducer = (state = initState, action) => {
    switch (action.type) {

        case ACTION_TYPE.GET_STOCK_SUCCESS:
            return {
                ...state,
                getStockResponse: {
                    ...action.payload
                }
            };

        case ACTION_TYPE.GET_STOCK_FAILURE:
            //const errors = {};
            //Object.values(action.payload).forEach(obj => errors[obj.fieldName] = obj.text);
            return {
                ...state,
                getStockResponse: {
                    errors: action.payload
                }
            };

        case ACTION_TYPE.LIST_STOCK_SUCCESS:
            return {
                ...state,
                getAllStockResponse: {
                    items: action.payload
                }
            };

        case ACTION_TYPE.LIST_STOCK_FAILURE:
            //const errors = {};
            //Object.values(action.payload).forEach(obj => errors[obj.fieldName] = obj.text);
            return {
                ...state,
                getAllStockResponse: {
                    errors: action.payload
                }
            };

        case ACTION_TYPE.POST_NEWORDER_REQUEST:
            return {
                ...state,
                postNewOrderResponse: {}
            }

        case ACTION_TYPE.POST_NEWORDER_SUCCESS:
            return {
                ...state,
                postNewOrderResponse: {
                    redirectUrl: action.payload
                }
            };

        case ACTION_TYPE.POST_NEWORDER_FAILURE:
            //const errors = {};
            //Object.values(action.payload).forEach(obj => errors[obj.fieldName] = obj.text);
            return {
                ...state,
                postNewOrderResponse: action.payload
            };

        default:
            return state;
    }
};

export default rootReducer;