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
import CalendarToolbar from 'ClientApp/components/calendar/calendarToolbar';
import FlatButton from 'material-ui/FlatButton/FlatButton';

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
        const allViews = Object.keys(BigCalendar.Views).map(key => BigCalendar.Views[key]);
        const weights = [
            {
                name: 'Very Low',
                value: 5
            },
            {
                name: 'Low',
                value: 4
            },
            {
                name: 'Medium',
                value: 3
            },
            {
                name: 'High',
                value: 2
            },
            {
                name: 'Very High',
                value: 1
            },
        ];

        const { events } = this.props;
        const { selectedEvent } = this.state;
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
                    views={allViews}
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
                    open={this.state.isEventDialogOpen}
                    onRequestClose={this.handleEventDialogClose}>
                    <div className="modal-header">
                        <h4 className="modal-title">{selectedEvent.name}</h4>
                    </div>
                    <div className="modal-body">
                        <dl className="dl-horizontal">
                            <dt>Description</dt>
                            <dd>{selectedEvent.description !== null && selectedEvent.description !== '' ? selectedEvent.description : 'N/A'}</dd>
                            <dt>Parent</dt>
                            <dd>{selectedEvent.eventParentName}</dd>
                            <dt>Group</dt>
                            <dd>{selectedEvent.eventGroupName}</dd>
                            <dt>Type</dt>
                            <dd>{selectedEvent.eventTypeName}</dd>
                            <dt>Lasts all day</dt>
                            <dd>{selectedEvent.isAllDay ? 'Yes' : 'No'}</dd>
                            {selectedEvent.isAllDay &&
                                <dt>Date</dt>
                            }
                            {selectedEvent.isAllDay &&
                                <dd>{moment(selectedEvent.startDateTime).format('MMM DD, YYYY')}</dd>
                            }
                            {!selectedEvent.isAllDay &&
                                <dt>Start</dt>
                            }
                            {!selectedEvent.isAllDay &&
                                <dd>{moment(selectedEvent.startDateTime).format('MMM DD, YYYY LT')}</dd>
                            }
                            {!selectedEvent.isAllDay &&
                                <dt>End</dt>
                            }
                            {!selectedEvent.isAllDay &&
                                <dd>{moment(selectedEvent.endDateTime).format('MMM DD, YYYY LT')}</dd>
                            }
                            <dt>Importance</dt>
                            <dd>{selectedEvent.weight !== undefined &&
                                selectedEvent.weight !== '' &&
                                selectedEvent.weight !== null ? weights.find(weight => weight.value === selectedEvent.weight).name : 'N/A'}</dd>
                        </dl>
                    </div>
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
