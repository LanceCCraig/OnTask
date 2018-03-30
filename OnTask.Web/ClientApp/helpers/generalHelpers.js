/**
 * External depedencies
 */
import moment from 'moment';

/**
 * Internal depedencies
 */
import Constants from 'ClientApp/constants';

export function updateEventParentForApi(eventParent) {
    let newEventParent = Object.assign({}, eventParent);
    newEventParent.description = checkBlankReturnNull(eventParent.description);
    newEventParent.weight = checkBlankReturnNull(eventParent.weight);
    return newEventParent;
}

export function updateEventParentForDisplay(eventParent) {
    let newEventParent = Object.assign({}, eventParent);
    newEventParent.description = checkNullReturnBlank(eventParent.description);
    newEventParent.weight = checkNullReturnBlank(eventParent.weight);
    return newEventParent;
}

export function updateEventGroupForApi(eventGroup) {
    let newEventGroup = Object.assign({}, eventGroup);
    newEventGroup.description = checkBlankReturnNull(eventGroup.description);
    newEventGroup.weight = checkBlankReturnNull(eventGroup.weight);
    return newEventGroup;
}

export function updateEventGroupForDisplay(eventGroup) {
    let newEventGroup = Object.assign({}, eventGroup);
    newEventGroup.description = checkNullReturnBlank(eventGroup.description);
    newEventGroup.weight = checkNullReturnBlank(eventGroup.weight);
    return newEventGroup;
}

export function updateEventTypeForApi(eventType) {
    let newEventType = Object.assign({}, eventType);
    newEventType.description = checkBlankReturnNull(eventType.description);
    newEventType.weight = checkBlankReturnNull(eventType.weight);
    return newEventType;
}

export function updateEventTypeForDisplay(eventType) {
    let newEventType = Object.assign({}, eventType);
    newEventType.description = checkNullReturnBlank(eventType.description);
    newEventType.weight = checkNullReturnBlank(eventType.weight);
    return newEventType;
}

export function updateEventForApi(event) {
    let newEvent = Object.assign({}, event);
    newEvent.description = checkBlankReturnNull(event.description);
    newEvent.startDate = event.startDate !== null ? moment(event.startDate).format(Constants.MOMENT_DATE_FORMAT) : null;
    newEvent.startTime = event.startTime !== null ? moment(event.startTime).format(Constants.MOMENT_TIME_FORMAT) + ':00' : null;
    newEvent.endDate = event.endDate !== null ? moment(event.endDate).format(Constants.MOMENT_DATE_FORMAT) : null;
    newEvent.endTime = event.endTime !== null ? moment(event.endTime).format(Constants.MOMENT_TIME_FORMAT) + ':00' : null;
    newEvent.weight = checkBlankReturnNull(event.weight);
    return newEvent;
}

export function updateEventForDisplay(event) {
    let newEvent = Object.assign({}, event);
    newEvent.description = checkNullReturnBlank(event.description);
    newEvent.startDate = event.startDate !== null ? moment(event.startDate) : null;
    newEvent.startTime = event.startTime !== null ? moment(event.startTime) : null;
    newEvent.endDate = event.endDate !== null ? moment(event.endDate) : null;
    newEvent.endTime = event.endTime !== null ? moment(event.endTime) : null;
    newEvent.weight = checkNullReturnBlank(event.weight);
    return newEvent;
}

export function updateRecurringEventForApi(recurringEvent) {
    let newRecurringEvent = Object.assign({}, recurringEvent);
    newRecurringEvent.description = checkBlankReturnNull(recurringEvent.description);
    newRecurringEvent.startTime = recurringEvent.startTime !== null ? moment(recurringEvent.startTime).format(Constants.MOMENT_TIME_FORMAT) + ':00' : null;
    newRecurringEvent.endTime = recurringEvent.endTime !== null ? moment(recurringEvent.endTime).format(Constants.MOMENT_TIME_FORMAT) + ':00' : null;
    newRecurringEvent.dateRangeStart = recurringEvent.dateRangeStart !== null ? moment(recurringEvent.dateRangeStart).format(Constants.MOMENT_DATE_FORMAT) : null;
    newRecurringEvent.dateRangeEnd = recurringEvent.dateRangeEnd !== null ? moment(recurringEvent.dateRangeEnd).format(Constants.MOMENT_DATE_FORMAT) : null;
    newRecurringEvent.weight = checkBlankReturnNull(recurringEvent.weight);
    return newRecurringEvent;
}

function checkBlankReturnNull(item) {
    return item === '' ? null : item;
}

function checkNullReturnBlank(item) {
    return item === null ? '' : item;
}
