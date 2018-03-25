/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function eventParentReducer(state = initialState.eventParents, action) {
    switch (action.type) {
        case types.CREATE_EVENT_PARENT_SUCCESS:
            return [
                ...state,
                Object.assign({}, action.eventParent)
            ];
        case types.DELETE_EVENT_PARENT_SUCCESS:
            return [
                ...state.filter(eventParent => eventParent.id !== action.id)
            ];
        case types.GET_ALL_EVENT_PARENT_SUCCESS:
            return action.eventParents;
        case types.UPDATE_EVENT_PARENT_SUCCESS:
            return [
                ...state.filter(eventParent => eventParent.id !== action.eventParent.id),
                Object.assign({}, action.eventParent)
            ];
        default:
            return state;
    }
}
