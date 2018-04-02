/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function authReducer(state = initialState.auth, action) {
    switch (action.type) {
        case types.LOGIN_REQUEST:
            return {
                ...state,
                loggingIn: true,
                user: action.user
            };
        case types.LOGIN_SUCCESS:
            return {
                ...state,
                loggedIn: true,
                loggingIn: false,
                user: action.user
            };
        case types.LOGIN_FAILURE:
            return {
                ...state,
                loggingIn: false,
                errors: action.errors
            };
        case types.LOGOUT:
            // Clear user data from the store.
            return initialState;
        default:
            return state;
    }
}
