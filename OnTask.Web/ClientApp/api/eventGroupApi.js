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

let EventGroupApi = {
    create(eventParentId, name, description, weight) {
        let options = {
            method: 'POST',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                eventParentId,
                name,
                description,
                weight
            })
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    delete(id) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    deleteMultiple(eventParentId) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                eventParentId
            })
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    getAll(eventParentId) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };
        let parameters = {
            eventParentId
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}?${queryString.stringify(parameters)}`, options)
            .then(handleResponse, handleError);
    },

    getById(id) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    update(id, eventParentId, name, description, weight) {
        let options = {
            method: 'PUT',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                id,
                eventParentId,
                name,
                description,
                weight
            })
        };

        return fetch(`${Constants.EVENT_GROUP_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    }
};

export default EventGroupApi;
