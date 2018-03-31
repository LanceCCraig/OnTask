/**
 * External dependencies
 */
import React from 'react';
import DropDownMenu from 'material-ui/DropDownMenu';
import IconButton from 'material-ui/IconButton';
import MenuItem from 'material-ui/MenuItem';
import {
    Toolbar,
    ToolbarGroup,
    ToolbarSeparator,
    ToolbarTitle
} from 'material-ui/Toolbar';
import HardwareKeyboardArrowLeft from 'material-ui/svg-icons/hardware/keyboard-arrow-left';
import ActionDateRange from 'material-ui/svg-icons/action/date-range';
import HardwareKeyboardArrowRight from 'material-ui/svg-icons/hardware/keyboard-arrow-right';

const CalendarToolbar = (toolbar) => {
    const availableViews = [
        {
            name: 'Month',
            value: 'month'
        },
        {
            name: 'Week',
            value: 'week'
        },
        {
            name: 'Day',
            value: 'day'
        }
    ];
    const handleBackClick = () => {
        toolbar.onNavigate('PREV');
    }
    const handleTodayClick = () => {
        toolbar.onNavigate('TODAY');
    }
    const handleNextClick = () => {
        toolbar.onNavigate('NEXT');
    }
    const handleViewChange = (e, index, value) => {
        toolbar.onViewChange(value);
    }

    return (
        <Toolbar>
            <ToolbarGroup firstChild={true}>
                <IconButton
                    onClick={handleBackClick}
                    tooltip="Back">
                    <HardwareKeyboardArrowLeft />
                </IconButton>
                <IconButton
                    className="rbc-custom-next"
                    onClick={handleNextClick}
                    tooltip="Next">
                    <HardwareKeyboardArrowRight />
                </IconButton>
                <IconButton
                    className="hidden-xs"
                    onClick={handleTodayClick}
                    tooltip="Today">
                    <ActionDateRange />
                </IconButton>
            </ToolbarGroup>
            <ToolbarGroup>
                <ToolbarTitle
                    style={{ color: 'black' }}
                    text={toolbar.label}
                />
            </ToolbarGroup>
            <ToolbarGroup lastChild={true}>
                <DropDownMenu
                    value={toolbar.view}
                    onChange={handleViewChange}>
                    {availableViews.map(view =>
                        <MenuItem key={view.value} value={view.value} primaryText={view.name} />
                    )}
                </DropDownMenu>
            </ToolbarGroup>
        </Toolbar>
    );
};

export default CalendarToolbar;
