/**
 * External dependencies
 */
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import eventTypeApi from 'ClientApp/api/eventTypeApi';
import { updateEventTypeForApi } from 'ClientApp/helpers/generalHelpers';

export function createType(eventType) {
    return function(dispatch) {
        let createdEventType = updateEventTypeForApi(eventType);

        return eventTypeApi.create(
            createdEventType.eventGroupId,
            createdEventType.eventParentId,
            createdEventType.name,
            createdEventType.description,
            createdEventType.weight,
            createdEventType.isRecommended).then(
            eventType => {
                dispatch(success(eventType));
                toastr.success('Type created.');
            },
            errors => {
                toastr.error('Type creation failed.');
            });
    };

    function success(eventType) { return { type: types.CREATE_EVENT_TYPE_SUCCESS, eventType }; }
}

export function deleteType(id) {
    return function (dispatch) {
        return eventTypeApi.delete(id).then(
            eventType => {
                dispatch(success(id));
                toastr.success('Type deleted.');
            },
            errors => {
                toastr.error('Type deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_TYPE_SUCCESS, id }; }
}

/*export function deleteMultipleTypes(mode, eventGroupId, eventParentId) {
    return function (dispatch) {
        return eventTypeApi.deleteMultiple(eventParentId).then(
            eventType => {
                dispatch(success(id));
                toastr.success('Types deleted.');
            },
            errors => {
                toastr.error('Type deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_TYPE_SUCCESS, id }; }
}*/

export function getAllTypes(eventGroupId, eventParentId) {
    return function(dispatch) {
        return eventTypeApi.getAll(eventGroupId, eventParentId).then(
            eventTypes => {
                dispatch(success(eventTypes));
            },
            errors => {
                // Do nothing.
            });
    };

    function success(eventTypes) { return { type: types.GET_ALL_EVENT_TYPE_SUCCESS, eventTypes }; }
}

export function updateType(eventType) {
    return function(dispatch) {
        let updatedEventType = updateEventTypeForApi(eventType);

        return eventTypeApi.update(
            updatedEventType.id,
            updatedEventType.eventGroupId,
            updatedEventType.eventParentId,
            updatedEventType.name,
            updatedEventType.description,
            updatedEventType.weight,
            updatedEventType.isRecommended).then(
            () => {
                dispatch(success(eventType));
                toastr.success('Type updated.');
            },
            errors => {
                toastr.error('Type update failed.');
            }
        );
    };

    function success(eventType) { return { type: types.UPDATE_EVENT_TYPE_SUCCESS, eventType }; }
}
