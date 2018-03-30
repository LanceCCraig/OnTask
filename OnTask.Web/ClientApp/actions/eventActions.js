/**
 * External dependencies
 */
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import eventApi from 'ClientApp/api/eventApi';
import { updateEventForApi, updateEventsForCalendar, updateRecurringEventForApi } from 'ClientApp/helpers/generalHelpers';

export function createEvent(event) {
    return function (dispatch) {
        let createdEvent = updateEventForApi(event);

        return eventApi.create(
            createdEvent.eventTypeId,
            createdEvent.eventGroupId,
            createdEvent.eventParentId,
            createdEvent.name,
            createdEvent.description,
            createdEvent.startDate,
            createdEvent.startTime,
            createdEvent.endDate,
            createdEvent.endTime,
            createdEvent.isAllDay,
            createdEvent.weight).then(
            event => {
                dispatch(success(event));
                toastr.success('Event created.');
            },
            errors => {
                toastr.error('Event creation failed.');
            });
    };

    function success(event) { return { type: types.CREATE_EVENT_SUCCESS, event }; }
}

export function createRecurringEvent(recurringEvent) {
    return function (dispatch) {
        let createdRecurringEvent = updateRecurringEventForApi(recurringEvent);

        return eventApi.createRecurring(
            createdRecurringEvent.eventTypeId,
            createdRecurringEvent.eventGroupId,
            createdRecurringEvent.eventParentId,
            createdRecurringEvent.name,
            createdRecurringEvent.description,
            createdRecurringEvent.weight,
            createdRecurringEvent.startTime,
            createdRecurringEvent.endTime,
            createdRecurringEvent.dateRangeStart,
            createdRecurringEvent.dateRangeEnd,
            createdRecurringEvent.isAllDay,
            createdRecurringEvent.daysOfWeek).then(
            events => {
                dispatch(success(events));
                toastr.success('Recurring event(s) created.');
            },
            errors => {
                toastr.error('Recurring event creation failed.');
            });
    };

    function success(events) { return { type: types.CREATE_RECURRING_EVENT_SUCCESS, events }; }
}

export function deleteEvent(id) {
    return function (dispatch) {
        return eventApi.delete(id).then(
            event => {
                dispatch(success(id));
                toastr.success('Event deleted.');
            },
            errors => {
                toastr.error('Event deletion failed.');
            });
    };

    function success(id) { return { type: types.DELETE_EVENT_SUCCESS, id }; }
}

export function getAllEvents(
    eventTypeId,
    eventGroupId,
    eventParentId,
    dateRangeStart,
    dateRangeEnd) {
    return function (dispatch) {
        return eventApi.getAll(
            eventTypeId,
            eventGroupId,
            eventParentId,
            dateRangeStart,
            dateRangeEnd).then(
            events => {
                let retrievedEvents = updateEventsForCalendar(events);
                dispatch(success(retrievedEvents));
            },
            errors => {
                // Do nothing.
            });
    };

    function success(events) { return { type: types.GET_ALL_EVENT_SUCCESS, events }; }
}

export function updateEvent(event) {
    return function (dispatch) {
        let updatedEvent = updateEventForApi(event);

        return eventApi.update(
            updatedEvent.id,
            updatedEvent.eventTypeId,
            updatedEvent.eventGroupId,
            updatedEvent.eventParentId,
            updatedEvent.name,
            updatedEvent.description,
            updatedEvent.startDate,
            updatedEvent.startTime,
            updatedEvent.endDate,
            updatedEvent.endTime,
            updatedEvent.isAllDay,
            updatedEvent.weight).then(
            () => {
                dispatch(success(event));
                toastr.success('Event updated.');
            },
            errors => {
                toastr.error('Event update failed.');
            }
        );
    };

    function success(event) { return { type: types.UPDATE_EVENT_SUCCESS, event }; }
}
