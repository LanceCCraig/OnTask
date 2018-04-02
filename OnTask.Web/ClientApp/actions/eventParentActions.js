/**
 * External dependencies
 */
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import eventParentApi from 'ClientApp/api/eventParentApi';
import { updateEventParentForApi } from 'ClientApp/helpers/generalHelpers';

export function createParent(eventParent) {
    return function(dispatch) {
        let createdEventParent = updateEventParentForApi(eventParent);

        return eventParentApi.create(createdEventParent.name, createdEventParent.description, createdEventParent.weight).then(
            eventParent => {
                dispatch(success(eventParent));
                toastr.success('Parent created.');
            },
            errors => {
                toastr.error('Parent creation failed.');
            });
    };

    function success(eventParent) { return { type: types.CREATE_EVENT_PARENT_SUCCESS, eventParent }; }
}

export function deleteParent(id) {
    return function(dispatch) {
        return eventParentApi.delete(id).then(
            eventParent => {
                dispatch(success(id));
                toastr.success('Parent deleted.');
            },
            errors => {
                toastr.error('Parent deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_PARENT_SUCCESS, id }; }
}

export function getAllParents() {
    return function(dispatch) {
        return eventParentApi.getAll().then(
            eventParents => {
                dispatch(success(eventParents));
            },
            errors => {
                // Do nothing.
            });
    };

    function success(eventParents) { return { type: types.GET_ALL_EVENT_PARENT_SUCCESS, eventParents }; }
}

export function updateParent(eventParent) {
    return function(dispatch) {
        let updatedEventParent = updateEventParentForApi(eventParent);

        return eventParentApi.update(updatedEventParent.id, updatedEventParent.name, updatedEventParent.description, updatedEventParent.weight).then(
            () => {
                dispatch(success(eventParent));
                toastr.success('Parent updated.');
            },
            errors => {
                toastr.error('Parent update failed.');
            }
        );
    };

    function success(eventParent) { return { type: types.UPDATE_EVENT_PARENT_SUCCESS, eventParent }; }
}
