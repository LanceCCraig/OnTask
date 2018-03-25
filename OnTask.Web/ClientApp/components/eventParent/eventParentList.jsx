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
import EventParentListRow from 'ClientApp/components/eventParent/eventParentListRow';

const EventParentList = ({ eventParents, selectedIds, handleRowSelection }) => {
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
                        <TableHeaderColumn>Weight</TableHeaderColumn>
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={true}
                    showRowHover={true}>
                    {eventParents.map(eventParent =>
                        <EventParentListRow
                            key={eventParent.id}
                            eventParent={eventParent}
                            selected={selectedIds.includes(eventParent.id)}
                        />
                    )}
                </TableBody>
            </Table>
        </div>
    );
}

EventParentList.propTypes = {
    eventParents: PropTypes.array.isRequired,
    selectedIds: PropTypes.array.isRequired,
    handleRowSelection: PropTypes.func.isRequired
};

export default EventParentList;
