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


const EventParentListRow = ({ eventParent, selected, ...otherProps }) => {
    return (
        <TableRow
            {...otherProps}>
            {otherProps.children[0]}
            <TableRowColumn><Link to={'eventParent/' + eventParent.id}>{eventParent.name}</Link></TableRowColumn>
            <TableRowColumn>{eventParent.description}</TableRowColumn>
        </TableRow>
    );
}

EventParentListRow.propTypes = {
    eventParent: PropTypes.object.isRequired,
    selected: PropTypes.bool.isRequired,
    otherProps: PropTypes.array
};

export default EventParentListRow;
