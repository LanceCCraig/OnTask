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

let EventTypeApi = {
    create(eventGroupId, eventParentId, name, description, weight, isRecommended) {
        let options = {
            method: 'POST',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                eventGroupId,
                eventParentId,
                name,
                description,
                weight,
                isRecommended
            })
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    delete(id) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    deleteMultiple(mode, eventGroupId, eventParentId) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                mode,
                eventGroupId,
                eventParentId
            })
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    getAll(eventGroupId, eventParentId) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };
        let parameters = {
            eventGroupId,
            eventParentId
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}?${queryString.stringify(parameters)}`, options)
            .then(handleResponse, handleError);
    },

    getById(id) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    update(id, eventGroupId, eventParentId, name, description, weight, isRecommended) {
        let options = {
            method: 'PUT',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                id,
                eventGroupId,
                eventParentId,
                name,
                description,
                weight,
                isRecommended
            })
        };

        return fetch(`${Constants.EVENT_TYPE_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    }
};

export default EventTypeApi;
