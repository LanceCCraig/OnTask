/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import recommendationApi from 'ClientApp/api/recommendationApi';
import { updateRecommendationsForCalendar } from 'ClientApp/helpers/generalHelpers';

export function getRecommendations(end, mode) {
    return function (dispatch) {
        return recommendationApi.getAll(end, mode).then(
            recommendations => {
                let calendarRecommendations = updateRecommendationsForCalendar(recommendations);
                dispatch(success(calendarRecommendations));
            },
            errors => {
                // Do nothing.
            });
    };

    function success(recommendations) { return { type: types.GET_RECOMMENDATIONS_SUCCESS, recommendations }; }
}
