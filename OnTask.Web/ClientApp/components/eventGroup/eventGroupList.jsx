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

const EventGroupList = ({ eventGroups, eventParent, selectedIds, handleRowSelection }) => {
    return (
        <div>
            <Table
                fixedHeader={true}
                multiSelectable={true}
                onRowSelection={handleRowSelection}
                selectable={true}>
                <TableHeader
                    adjustForCheckbox={true}
                    displaySelectAll={false}>
                    <TableRow>
                        <TableHeaderColumn>Name</TableHeaderColumn>
                        <TableHeaderColumn>Description</TableHeaderColumn>
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={true}
                    showRowHover={true}>
                    {eventGroups.filter(eventGroup => {
                        return eventGroup.eventParentId === eventParent.id || eventParent.id == '';
                    }).map(eventGroup =>
                        <EventGroupListRow
                            key={eventGroup.id}
                            eventGroup={eventGroup}
                            selected={selectedIds.includes(eventGroup.id)}
                        />
                    )}
                </TableBody>
            </Table>
        </div>
    );
}

EventGroupList.propTypes = {
    eventGroups: PropTypes.array.isRequired,
    eventParent: PropTypes.object.isRequired,
    selectedIds: PropTypes.array.isRequired,
    handleRowSelection: PropTypes.func.isRequired
};

export default EventGroupList;
