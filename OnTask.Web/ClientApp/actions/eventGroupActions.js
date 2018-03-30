/**
 * External dependencies
 */
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import eventGroupApi from 'ClientApp/api/eventGroupApi';
import { updateEventGroupForApi } from 'ClientApp/helpers/generalHelpers';

export function createGroup(eventGroup) {
    return function(dispatch) {
        let createdEventGroup = updateEventGroupForApi(eventGroup);

        return eventGroupApi.create(createdEventGroup.eventParentId, createdEventGroup.name, createdEventGroup.description, createdEventGroup.weight).then(
            eventGroup => {
                dispatch(success(eventGroup));
                toastr.success('Group created.');
            },
            errors => {
                toastr.error('Group creation failed.');
            });
    };

    function success(eventGroup) { return { type: types.CREATE_EVENT_GROUP_SUCCESS, eventGroup }; }
}

export function deleteGroup(id) {
    return function (dispatch) {
        return eventGroupApi.delete(id).then(
            eventGroup => {
                dispatch(success(id));
                toastr.success('Group deleted.');
            },
            errors => {
                toastr.error('Group deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_GROUP_SUCCESS, id }; }
}

/*export function deleteMultipleGroups(eventParentId) {
    return function (dispatch) {
        return eventGroupApi.deleteMultiple(eventParentId).then(
            eventGroup => {
                dispatch(success(id));
                toastr.success('Groups deleted.');
            },
            errors => {
                toastr.error('Group deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_GROUP_SUCCESS, id }; }
}*/

export function getAllGroups(eventParentId) {
    return function(dispatch) {
        return eventGroupApi.getAll(eventParentId).then(
            eventGroups => {
                dispatch(success(eventGroups));
            },
            errors => {
                // Do nothing.
            });
    };

    function success(eventGroups) { return { type: types.GET_ALL_EVENT_GROUP_SUCCESS, eventGroups }; }
}

export function updateGroup(eventGroup) {
    return function(dispatch) {
        let updatedEventGroup = updateEventGroupForApi(eventGroup);

        return eventGroupApi.update(updatedEventGroup.id, updatedEventGroup.eventParentId, updatedEventGroup.name, updatedEventGroup.description, updatedEventGroup.weight).then(
            () => {
                dispatch(success(eventGroup));
                toastr.success('Group updated.');
            },
            errors => {
                toastr.error('Group update failed.');
            }
        );
    };

    function success(eventGroup) { return { type: types.UPDATE_EVENT_GROUP_SUCCESS, eventGroup }; }
}
