/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import {
    TableRow,
    TableRowColumn
} from 'material-ui/Table';


const EventGroupListRow = ({ eventGroup, selected, ...otherProps }) => {
    return (
        <TableRow
            {...otherProps}>
            {otherProps.children[0]}
            <TableRowColumn><Link to={'eventGroup/' + eventGroup.id}>{eventGroup.name}</Link></TableRowColumn>
            <TableRowColumn>{eventGroup.eventParentId}</TableRowColumn>
            <TableRowColumn>{eventGroup.description}</TableRowColumn>
            <TableRowColumn>{eventGroup.weight}</TableRowColumn>
        </TableRow>
    );
}

EventGroupListRow.propTypes = {
    eventGroup: PropTypes.object.isRequired,
    selected: PropTypes.bool.isRequired,
    otherProps: PropTypes.array
};

export default EventGroupListRow;
