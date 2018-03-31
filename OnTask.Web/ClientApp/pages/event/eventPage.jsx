/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import moment from 'moment';
import toastr from 'toastr';
import DatePicker from 'material-ui/DatePicker';
import RaisedButton from 'material-ui/RaisedButton';
import SelectField from 'material-ui/SelectField';
import TextField from 'material-ui/TextField';
import TimePicker from 'material-ui/TimePicker';
import Toggle from 'material-ui/Toggle';

/**
 * Internal dependencies
 */
import * as eventActions from 'ClientApp/actions/eventActions';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import GroupSelectField from 'ClientApp/components/common/groupSelectField';
import TypeSelectField from 'ClientApp/components/common/typeSelectField';
import { updateEventForDisplay } from 'ClientApp/helpers/generalHelpers';

class EventPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            event: updateEventForDisplay(props.event),
            errors: {},
            saving: false
        };
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.event.id !== nextProps.event.id) {
            this.setState({ event: updateEventForDisplay(nextProps.event) });
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.setState({ saving: true });
        if (this.props.event.id === '') {
            this.props.actions.createEvent(this.state.event)
                .then(() => this.redirectToCalendarPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        } else {
            this.props.actions.updateEvent(this.state.event)
                .then(() => this.redirectToCalendarPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        }
    }

    redirectToCalendarPage() {
        this.setState({ saving: false });
        this.props.routerActions.push('/calendar');
    }

    handleTextChange = e => {
        const { name, value } = e.currentTarget;
        let event = Object.assign({}, this.state.event);
        event[name] = value;
        return this.setState({ event: event });
    }

    handleParentChange = (e, index, value) => {
        let event = Object.assign({}, this.state.event);
        // Reset the group and type filter if the parent filter is changed.
        if (value !== event.eventParentId) {
            event.eventGroupId = '';
            event.eventTypeId = '';
        }
        event.eventParentId = value;
        return this.setState({ event: event });
    }

    handleGroupChange = (e, index, value) => {
        let event = Object.assign({}, this.state.event);
        // Reset the type filter if the group filter is changed.
        if (value !== event.eventGroupId) {
            event.eventTypeId = '';
        }
        event.eventGroupId = value;
        return this.setState({ event: event });
    }

    handleTypeChange = (e, index, value) => {
        let event = Object.assign({}, this.state.event);
        event.eventTypeId = value;
        return this.setState({ event: event });
    }

    handleIsAllDayToggle = (e, isChecked) => {
        let event = Object.assign({}, this.state.event);
        if (isChecked) {
            event.endDate = event.startDate;
            event.startTime = null;
            event.endTime = null;
        } else {
            event.endDate = null;
        }
        event.isAllDay = isChecked;
        return this.setState({ event: event });
    }

    handleDateOrTimeChange = (name, value) => {
        let event = Object.assign({}, this.state.event);
        event[name] = value;
        return this.setState({ event: event });
    }

    handleStartDateChange = (e, date) => {
        let event = Object.assign({}, this.state.event);
        event.startDate = date;
        // Set the end date for all day events since the form element will be hidden from the user.
        if (this.state.event.isAllDay) {
            event.endDate = date;
        }
        return this.setState({ event: event });
    }

    handleStartTimeChange = (e, time) => this.handleDateOrTimeChange('startTime', time);

    handleEndDateChange = (e, date) => this.handleDateOrTimeChange('endDate', date);

    handleEndTimeChange = (e, time) => this.handleDateOrTimeChange('endTime', time);

    handleWeightChange = (e, index, value) => {
        let event = Object.assign({}, this.state.event);
        event.weight = value;
        return this.setState({ event: event });
    }

    hasRequiredFields() {
        const { event } = this.state;

        let missingDatesAndTimesForNonAllDay = !event.isAllDay && (
            event.startTime === null ||
            event.endDate === null ||
            event.endTime === null);
        if (event.eventParentId === '' ||
            event.eventGroupId === '' ||
            event.eventTypeId === '' ||
            event.name === '' ||
            event.startDate === null ||
            missingDatesAndTimesForNonAllDay) {
            return false;
        }
        return true;
    }

    canSubmitForm = () => {
        if (this.state.saving) {
            return false;
        }
        return this.hasRequiredFields();
    }

    isGroupFilterDisabled = () => this.state.event.eventParentId === null || this.state.event.eventParentId === '';

    isTypeFilterDisabled = () => this.state.event.eventGroupId === null || this.state.event.eventGroupId === '';

    render() {
        const { eventParents, eventGroups, eventTypes } = this.props;
        const { event, errors, saving } = this.state;
        const toggleLabelStyle = {
            color: '#B2B2B2',
            textAlign: 'left'
        };
        const toggleStyle = {
            display: 'inline-block',
            fontFamily: '"Roboto", sans-serif',
            fontSize: 16,
            fontWeight: 700,
            marginTop: 34,
            maxWidth: 256,
            minHeight: 20
        };
        
        return (
            <div style={{ textAlign: "center" }}>
                <h1>Event</h1>
                <form>
                    <ParentSelectField
                        name="eventParentId"
                        eventParents={eventParents}
                        disabled={saving}
                        value={event.eventParentId}
                        onChange={this.handleParentChange}
                        errorText={errors.eventParentId}
                    /><br />
                    <GroupSelectField
                        name="eventGroupId"
                        eventGroups={eventGroups}
                        eventParentId={event.eventParentId}
                        disabled={this.isGroupFilterDisabled() || saving}
                        value={event.eventGroupId}
                        onChange={this.handleGroupChange}
                        errorText={errors.eventGroupId}
                    /><br />
                    <TypeSelectField
                        name="eventTypeId"
                        eventTypes={eventTypes}
                        eventParentId={event.eventParentId}
                        eventGroupId={event.eventGroupId}
                        disabled={this.isTypeFilterDisabled() || saving}
                        value={event.eventTypeId}
                        onChange={this.handleTypeChange}
                        errorText={errors.eventTypeId}
                    /><br />
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={event.name}
                        onChange={this.handleTextChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={event.description}
                        onChange={this.handleTextChange}
                        errorText={errors.description}
                    /><br />
                    <Toggle
                        name="allDay"
                        label="All day event"
                        labelStyle={toggleLabelStyle}
                        disabled={saving}
                        toggled={event.isAllDay}
                        onToggle={this.handleIsAllDayToggle}
                        style={toggleStyle}
                    /><br />
                    <DatePicker
                        name="startDate"
                        floatingLabelText={!event.isAllDay ? "Start date" : "Date"}
                        disabled={saving}
                        value={event.startDate}
                        onChange={this.handleStartDateChange}
                        firstDayOfWeek={0}
                    />
                    {!event.isAllDay &&
                        <TimePicker
                            name="startTime"
                            floatingLabelText="Start time"
                            disabled={saving}
                            value={event.startTime}
                            onChange={this.handleStartTimeChange}
                        />
                    }
                    {!event.isAllDay &&
                        <DatePicker
                            name="endDate"
                            floatingLabelText="End date"
                            disabled={saving}
                            value={event.endDate}
                            onChange={this.handleEndDateChange}
                            firstDayOfWeek={0}
                        />
                    }
                    {!event.isAllDay &&
                        <TimePicker
                            name="endTime"
                            floatingLabelText="End time"
                            disabled={saving}
                            value={event.endTime}
                            onChange={this.handleEndTimeChange}
                        />
                    }
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={event.weight}
                        onChange={this.handleWeightChange}
                        errorText={errors.weight}
                    /><br />
                    <RaisedButton
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        label={saving ? "Saving..." : "Save"}
                        labelStyle={{ color: 'white' }}
                        backgroundColor="#2DB1FF"
                        rippleStyle={{ backgroundColor: "#005C93" }}
                        onClick={this.submitForm}
                    />
                </form>
            </div>
        );
    }
}

EventPage.propTypes = {
    event: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    eventGroups: PropTypes.array.isRequired,
    eventTypes: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function getEventById(events, id) {
    const event = events.filter(event => event.id == id);
    return event.length ? event[0] : null;
}

function mapStateToProps(state, ownProps) {
    const eventId = ownProps.match.params.id;
    let event = {
        id: '',
        eventTypeId: '',
        eventGroupId: '',
        eventParentId: '',
        name: '',
        description: '',
        startDate: null,
        startTime: null,
        endDate: null,
        endTime: null,
        isAllDay: false,
        weight: ''
    };

    if (eventId && state.events.length > 0) {
        event = getEventById(state.events, eventId);
    }

    return {
        event,
        eventParents: state.eventParents,
        eventGroups: state.eventGroups,
        eventTypes: state.eventTypes
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventPage);
