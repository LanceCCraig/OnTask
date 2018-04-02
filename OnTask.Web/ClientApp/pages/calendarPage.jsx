/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import BigCalendar from 'react-big-calendar';
import Dialog from 'material-ui/Dialog';
import moment from 'moment';

/**
 * Internal dependencies
 */
import * as eventActions from 'ClientApp/actions/eventActions';
import CalendarDialogContent from 'ClientApp/components/calendar/calendarDialogContent';
import CalendarToolbar from 'ClientApp/components/calendar/calendarToolbar';
import FlatButton from 'material-ui/FlatButton';

BigCalendar.setLocalizer(BigCalendar.momentLocalizer(moment))

class CalendarPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            isEventDialogOpen: false,
            selectedEvent: {}
        };
    }

    handleSelectEvent = (event, e) => {
        this.setState({ selectedEvent: event });
        this.handleEventDialogOpen();
    }

    handleEventDialogOpen = () => {
        this.setState({ isEventDialogOpen: true });
    }

    handleEventDialogClose = () => {
        this.setState({ isEventDialogOpen: false });
    }

    handleEventEditClick = () => {
        this.props.routerActions.push('event/' + this.state.selectedEvent.id);
    }

    handleEventDeleteClick = () => {
        this.props.actions.deleteEvent(this.state.selectedEvent.id);
        this.handleEventDialogClose();
    }

    render() {
        const views = ['month', 'week', 'day'];

        const { events } = this.props;
        const { isEventDialogOpen, selectedEvent } = this.state;
        const actions = [
            <FlatButton
                secondary
                label="Delete"
                onClick={this.handleEventDeleteClick}
            />,
            <FlatButton
                primary
                label="Edit"
                onClick={this.handleEventEditClick}
            />,
            <FlatButton
                label="Close"
                onClick={this.handleEventDialogClose}
            />
        ];
        return (
            <div className="calendar-container">
                <BigCalendar
                    events={events}
                    components={{
                        toolbar: CalendarToolbar
                    }}
                    views={views}
                    step={30}
                    popup={true}
                    selectable={true}
                    showMultiDayTimes={true}
                    toolbar={true}
                    defaultDate={new Date()}
                    titleAccessor="name"
                    startAccessor="startDateTime"
                    endAccessor="endDateTime"
                    allDayAccessor="isAllDay"
                    onSelectEvent={this.handleSelectEvent}
                />
                <Dialog
                    actions={actions}
                    modal={false}
                    open={isEventDialogOpen}
                    onRequestClose={this.handleEventDialogClose}>
                    <CalendarDialogContent
                        name={selectedEvent.name}
                        description={selectedEvent.description}
                        eventParentName={selectedEvent.eventParentName}
                        eventGroupName={selectedEvent.eventGroupName}
                        eventTypeName={selectedEvent.eventTypeName}
                        isAllDay={selectedEvent.isAllDay}
                        startDateTime={selectedEvent.startDateTime}
                        endDateTime={selectedEvent.endDateTime}
                        weight={selectedEvent.weight}
                    />
                </Dialog>
            </div>
        );
    }
}

CalendarPage.propTypes = {
    actions: PropTypes.object.isRequired,
    events: PropTypes.array,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        events: state.events
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(CalendarPage);
