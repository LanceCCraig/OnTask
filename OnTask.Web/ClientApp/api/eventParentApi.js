/**
 * External dependencies
 */
import 'isomorphic-fetch';

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';
import { getAuthorizedHeaders, handleResponse, handleError } from 'ClientApp/helpers/apiHelpers';

let EventParentApi = {
    create(name, description, weight) {
        let options = {
            method: 'POST',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                name,
                description,
                weight
            })
        };
        
        return fetch(`${Constants.EVENT_PARENT_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    delete(id) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_PARENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    getAll() {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_PARENT_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    getById(id) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_PARENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    update(id, name, description, weight) {
        let options = {
            method: 'PUT',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                id,
                name,
                description,
                weight
            })
        };

        return fetch(`${Constants.EVENT_PARENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    }
};

export default EventParentApi;
