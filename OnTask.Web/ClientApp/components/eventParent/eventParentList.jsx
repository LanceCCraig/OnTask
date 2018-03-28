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

const EventParentList = ({ eventParents, handleMenuOnChange }) => {
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
                        <TableHeaderColumn>Description</TableHeaderColumn>
                        <TableHeaderColumn />
                    </TableRow>
                </TableHeader>
                <TableBody
                    deselectOnClickaway={false}
                    displayRowCheckbox={false}
                    showRowHover={true}>
                    {eventParents.map(eventParent =>
                        <EventParentListRow
                            key={eventParent.id}
                            eventParent={eventParent}
                            handleMenuOnChange={handleMenuOnChange}
                        />
                    )}
                </TableBody>
            </Table>
        </div>
    );
}

EventParentList.propTypes = {
    eventParents: PropTypes.array.isRequired,
    handleMenuOnChange: PropTypes.func.isRequired
};

export default EventParentList;
