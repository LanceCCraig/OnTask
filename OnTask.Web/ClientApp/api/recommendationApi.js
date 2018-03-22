/**
 * External dependencies
 */
import 'isomorphic-fetch';
const queryString = require('query-string');

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';
import { getAuthorizedHeaders, handleResponse, handleError } from 'ClientApp/helpers/apiHelpers';

let RecommendationApi = {
    getAll(end) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };
        let parameters = {
            end
        };

        return fetch(`${Constants.RECOMMENDATION_API_URL}?${queryString.stringify(parameters)}`, options)
            .then(handleResponse, handleError);
    }
};

export default RecommendationApi;
