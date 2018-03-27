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

const EventGroupList = ({ eventGroups, eventParents, selectedIds, handleRowSelection }) => {
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
                        <TableHeaderColumn>Parent</TableHeaderColumn>
                        <TableHeaderColumn>Description</TableHeaderColumn>
                        <TableHeaderColumn>Weight</TableHeaderColumn>
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={true}
                    showRowHover={true}>
                    {eventGroups.map(eventGroup =>
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
    eventParents: PropTypes.array.isRequired,
    selectedIds: PropTypes.array.isRequired,
    handleRowSelection: PropTypes.func.isRequired
};

export default EventGroupList;
