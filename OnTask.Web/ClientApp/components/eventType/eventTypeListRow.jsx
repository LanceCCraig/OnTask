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

const EventTypeListRow = ({ eventType, handleMenuOnChange, ...otherProps }) => {
    return (
        <TableRow
            {...otherProps}>
            {otherProps.children[0]}
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventType.name}
                    trigger="click">
                    {eventType.name}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn className="hidden-xs">
                <Tooltip
                    size="big"
                    title={eventType.eventParentName}
                    trigger="click">
                    {eventType.eventParentName}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn>
                <Tooltip
                    size="big"
                    title={eventType.eventGroupName}
                    trigger="click">
                    {eventType.eventGroupName}
                </Tooltip>
            </TableRowColumn>
            <TableRowColumn className="hidden-xs">
                <Tooltip
                    size="big"
                    title={eventType.description}
                    trigger="click">
                    {eventType.description}
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
                        containerElement={<Link to={'eventType/' + eventType.id} />}
                    />
                    <MenuItem
                        key="delete"
                        primaryText="Delete"
                        value={eventType.id}
                    />
                </IconMenu>
            </TableRowColumn>
        </TableRow>
    );
}

EventTypeListRow.propTypes = {
    eventType: PropTypes.object.isRequired,
    handleMenuOnChange: PropTypes.func.isRequired,
    otherProps: PropTypes.array
};

export default EventTypeListRow;
