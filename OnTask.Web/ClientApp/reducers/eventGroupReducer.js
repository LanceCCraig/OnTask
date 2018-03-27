/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function eventGroupReducer(state = initialState.eventGroups, action) {
    switch (action.type) {
        case types.CREATE_EVENT_GROUP_SUCCESS:
            return [
                ...state,
                Object.assign({}, action.eventGroup)
            ];
        case types.DELETE_EVENT_GROUP_SUCCESS:
            return [
                ...state.filter(eventGroup => eventGroup.id !== action.id)
            ];
        case types.GET_ALL_EVENT_GROUP_SUCCESS:
            return action.eventGroups;
        case types.UPDATE_EVENT_GROUP_SUCCESS:
            return [
                ...state.filter(eventGroup => eventGroup.id !== action.eventGroup.id),
                Object.assign({}, action.eventGroup)
            ];
        default:
            return state;
    }
}
