/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import {
    Table,
    TableBody,
    TableHeader,
    TableHeaderColumn,
    TableRow
} from 'material-ui/Table';

/**
 * Internal dependencies
 */
import EventTypeListRow from 'ClientApp/components/eventType/eventTypeListRow';

const EventTypeList = ({ eventTypes, eventParentId, eventGroupId, handleMenuOnChange }) => {
    function filterTypes(element) {
        return (eventParentId === null || element.eventParentId === eventParentId) &&
            (eventGroupId === null || element.eventGroupId === eventGroupId);
    }

    return (
        <div style={{ marginBottom: '1%'}}>
            <Table
                fixedHeader={false}
                multiSelectable={false}
                selectable={false}>
                <TableHeader
                    adjustForCheckbox={false}
                    displaySelectAll={false}>
                    <TableRow>
                        <TableHeaderColumn>Name</TableHeaderColumn>
                        <TableHeaderColumn className="hidden-xs">Parent</TableHeaderColumn>
                        <TableHeaderColumn>Group</TableHeaderColumn>
                        <TableHeaderColumn className="hidden-xs">Description</TableHeaderColumn>
                        <TableHeaderColumn />
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={false}
                    showRowHover={true}>
                    {eventTypes.filter(filterTypes).map(eventType =>
                        <EventTypeListRow
                            key={eventType.id}
                            eventType={eventType}
                            handleMenuOnChange={handleMenuOnChange}
                        />
                    )}
                </TableBody>
            </Table>
        </div>
    );
}

EventTypeList.propTypes = {
    eventTypes: PropTypes.array.isRequired,
    eventParentId: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ]),
    eventGroupId: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ]),
    handleMenuOnChange: PropTypes.func.isRequired
};

export default EventTypeList;
