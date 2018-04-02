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
import IconMenu from 'material-ui/IconMenu';
import MenuItem from 'material-ui/MenuItem';
import IconButton from 'material-ui/IconButton';
import MoreVertIcon from 'material-ui/svg-icons/navigation/more-vert';
import { Tooltip } from 'react-tippy';

const EventParentListRow = ({ eventParent, handleMenuOnChange, ...otherProps }) => {
    return (
        <TableRow
            {...otherProps}>
            {otherProps.children[0]}
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventParent.name}
                    trigger="click">
                    {eventParent.name}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn className="hidden-xs">
                <Tooltip
                    size="big"
                    title={eventParent.description}
                    trigger="click">
                    {eventParent.description}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn style={{ textOverflow: 'clip' }}>
                <IconMenu
                    iconButtonElement={<IconButton><MoreVertIcon /></IconButton>}
                    anchorOrigin={{ horizontal: 'left', vertical: 'top' }}
                    targetOrigin={{ horizontal: 'left', vertical: 'top' }}
                    useLayerForClickAway={true}
                    onChange={handleMenuOnChange}>
                    <MenuItem
                        key="edit"
                        primaryText="Edit"
                        containerElement={<Link to={'eventParent/' + eventParent.id} />}
                    />
                    <MenuItem
                        key="delete"
                        primaryText="Delete"
                        value={eventParent.id}
                    />
                </IconMenu>
            </TableRowColumn>
        </TableRow>
    );
}

EventParentListRow.propTypes = {
    eventParent: PropTypes.object.isRequired,
    handleMenuOnChange: PropTypes.func.isRequired,
    otherProps: PropTypes.array
};

export default EventParentListRow;
