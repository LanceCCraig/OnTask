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
import EventGroupListRow from 'ClientApp/components/eventGroup/eventGroupListRow';

const EventGroupList = ({ eventGroups, eventParentId, handleMenuOnChange }) => {
    function filterGroups(element) {
        return eventParentId === null ||
            element.eventParentId === eventParentId;
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
                        <TableHeaderColumn>Parent</TableHeaderColumn>
                        <TableHeaderColumn className="hidden-xs">Description</TableHeaderColumn>
                        <TableHeaderColumn />
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={false}
                    showRowHover={true}>
                    {eventGroups.filter(filterGroups)
                        .map(eventGroup =>
                        <EventGroupListRow
                            key={eventGroup.id}
                            eventGroup={eventGroup}
                            handleMenuOnChange={handleMenuOnChange}
                        />
                    )}
                </TableBody>
            </Table>
        </div>
    );
}

EventGroupList.propTypes = {
    eventGroups: PropTypes.array.isRequired,
    eventParentId: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ]),
    handleMenuOnChange: PropTypes.func.isRequired
};

export default EventGroupList;
