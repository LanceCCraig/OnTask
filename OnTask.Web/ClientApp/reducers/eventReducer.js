/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function eventReducer(state = initialState.events, action) {
    switch (action.type) {
        case types.CREATE_EVENT_SUCCESS:
            return [
                ...state,
                Object.assign({}, action.event)
            ];
        case types.CREATE_RECURRING_EVENT_SUCCESS:
            return [
                ...state,
                ...action.events
            ];
        case types.DELETE_EVENT_SUCCESS:
            return [
                ...state.filter(event => event.id !== action.id)
            ];
        case types.GET_ALL_EVENT_SUCCESS:
            return action.events;
        case types.UPDATE_EVENT_SUCCESS:
            return [
                ...state.filter(event => event.id !== action.event.id),
                Object.assign({}, action.event)
            ];
        default:
            return state;
    }
}
