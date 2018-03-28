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

const EventGroupListRow = ({ eventGroup, handleMenuOnChange, ...otherProps }) => {
    return (
        <TableRow
            {...otherProps}>
            {otherProps.children[0]}
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventGroup.name}
                    trigger="click">
                    {eventGroup.name}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventGroup.eventParentName}
                    trigger="click">
                    {eventGroup.eventParentName}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventGroup.description}
                    trigger="click">
                    {eventGroup.description}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn>
                <IconMenu
                    iconButtonElement={<IconButton><MoreVertIcon /></IconButton>}
                    anchorOrigin={{ horizontal: 'left', vertical: 'top' }}
                    targetOrigin={{ horizontal: 'left', vertical: 'top' }}
                    useLayerForClickAway={true}
                    onChange={handleMenuOnChange}>
                    <MenuItem
                        key="edit"
                        primaryText="Edit"
                        containerElement={<Link to={'eventGroup/' + eventGroup.id} />}
                    />
                    <MenuItem
                        key="delete"
                        primaryText="Delete"
                        value={eventGroup.id}
                    />
                </IconMenu>
            </TableRowColumn>
        </TableRow>
    );
}

EventGroupListRow.propTypes = {
    eventGroup: PropTypes.object.isRequired,
    handleMenuOnChange: PropTypes.func.isRequired,
    otherProps: PropTypes.array
};

export default EventGroupListRow;
