/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function eventTypeReducer(state = initialState.eventTypes, action) {
    switch (action.type) {
        case types.CREATE_EVENT_TYPE_SUCCESS:
            return [
                ...state,
                Object.assign({}, action.eventType)
            ];
        case types.DELETE_EVENT_TYPE_SUCCESS:
            return [
                ...state.filter(eventType => eventType.id !== action.id)
            ];
        case types.GET_ALL_EVENT_TYPE_SUCCESS:
            return action.eventTypes;
        case types.UPDATE_EVENT_TYPE_SUCCESS:
            return [
                ...state.filter(eventType => eventType.id !== action.eventType.id),
                Object.assign({}, action.eventType)
            ];
        default:
            return state;
    }
}
