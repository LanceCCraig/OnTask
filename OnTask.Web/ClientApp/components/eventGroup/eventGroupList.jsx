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

const EventGroupList = ({ eventGroups, eventParent, handleMenuOnChange }) => {
    return (
        <div>
            <Table
                fixedHeader={true}
                multiSelectable={false}
                selectable={false}>
                <TableHeader
                    adjustForCheckbox={false}
                    displaySelectAll={false}>
                    <TableRow>
                        <TableHeaderColumn>Name</TableHeaderColumn>
                        <TableHeaderColumn>Parent</TableHeaderColumn>
                        <TableHeaderColumn>Description</TableHeaderColumn>
                        <TableHeaderColumn />
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={false}
                    showRowHover={true}>
                    {eventGroups.filter(eventGroup => {
                        return eventParent === null || eventParent.id === '' || eventGroup.eventParentId === eventParent.id;
                    }).map(eventGroup =>
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
    eventParent: PropTypes.object,
    handleMenuOnChange: PropTypes.func.isRequired
};

export default EventGroupList;
