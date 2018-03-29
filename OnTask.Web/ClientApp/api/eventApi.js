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

let EventApi = {
    create(eventTypeId, eventGroupId, eventParentId, name, description, startDate, endDate, weight) {
        let options = {
            method: 'POST',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                eventTypeId,
                eventGroupId,
                eventParentId,
                name,
                description,
                startDate,
                endDate,
                weight
            })
        };

        return fetch(`${Constants.EVENT_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    createRecurring(eventTypeId, eventGroupId, eventParentId, name, description, weight, startTime, endTime, startDate, endDate, daysOfWeek) {
        let options = {
            method: 'POST',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                eventTypeId,
                eventGroupId,
                eventParentId,
                name,
                description,
                weight,
                startTime,
                endTime,
                startDate,
                endDate,
                daysOfWeek
            })
        };

        return fetch(`${Constants.EVENT_API_URL}/CreateRecurring`, options)
            .then(handleResponse, handleError);
    },

    delete(id) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    deleteMultiple(mode, eventTypeId, eventGroupId, eventParentId) {
        let options = {
            method: 'DELETE',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                mode,
                eventTypeId,
                eventGroupId,
                eventParentId
            })
        };

        return fetch(`${Constants.EVENT_API_URL}`, options)
            .then(handleResponse, handleError);
    },

    getAll(eventTypeId, eventGroupId, eventParentId, dateRangeStart, dateRangeEnd) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };
        let parameters = {
            eventTypeId,
            eventGroupId,
            eventParentId,
            dateRangeStart,
            dateRangeEnd
        };

        return fetch(`${Constants.EVENT_API_URL}?${queryString.stringify(parameters)}`, options)
            .then(handleResponse, handleError);
    },

    getById(id) {
        let options = {
            method: 'GET',
            headers: getAuthorizedHeaders()
        };

        return fetch(`${Constants.EVENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    },

    update(id, eventTypeId, eventGroupId, eventParentId, name, description, startDate, endDate, weight) {
        let options = {
            method: 'PUT',
            headers: getAuthorizedHeaders(),
            body: JSON.stringify({
                id,
                eventTypeId,
                eventGroupId,
                eventParentId,
                name,
                description,
                startDate,
                endDate,
                weight
            })
        };

        return fetch(`${Constants.EVENT_API_URL}/${id}`, options)
            .then(handleResponse, handleError);
    }
};

export default EventApi;
