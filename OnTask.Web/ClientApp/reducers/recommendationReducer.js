/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import initialState from 'ClientApp/reducers/initialState';

export default function recommendationReducer(state = initialState.recommendations, action) {
    switch (action.type) {
        case types.GET_RECOMMENDATIONS_SUCCESS:
            return action.recommendations;
        default:
            return state;
    }
}
