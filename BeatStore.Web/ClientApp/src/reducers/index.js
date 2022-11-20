import { ACTION_TYPE } from 'actions';

const rootReducer = (state = {}, action) => {
    switch (action.type) {
        case ACTION_TYPE.LIST_TRACKS_SUCCESS:
            return {
                ...state,
                trackList: action.payload
            }


        case ACTION_TYPE.SEND_MESSAGE_REQUEST:
            return {
                ...state,
                contactMessageResult: {
                    isProcess: true,
                    isDone: false,
                    isSuccess: false
                }
            }

        case ACTION_TYPE.SEND_MESSAGE_SUCCESS:
            return {
                ...state,
                contactMessageResult: {
                    isProcess: false,
                    isDone: true,
                    isSuccess: true
                }
            }

        case ACTION_TYPE.SEND_MESSAGE_FAILURE:
            const errors = {};
            Object.values(action.payload).forEach(obj => errors[obj.fieldName] = obj.text);
            return {
                ...state,
                contactMessageResult: {
                    isProcess: false,
                    isDone: true,
                    isSuccess: false,
                    errors
                }
            }

        default:
            return state;
    }
};

export default rootReducer;