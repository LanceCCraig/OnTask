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
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import GroupSelectField from 'ClientApp/components/common/groupSelectField';
import TypeSelectField from 'ClientApp/components/common/typeSelectField';
import DaysOfWeekCheckboxList from 'ClientApp/components/common/daysOfWeekCheckboxList';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';

class RecurringEventPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            recurringEvent: props.recurringEvent,
            errors: {},
            saving: false
        };
    }

    submitForm = e => {
        e.preventDefault();
        this.setState({ saving: true });
        this.props.actions.createRecurringEvent(this.state.recurringEvent)
            .then(() => this.redirectToCalendarPage())
            .catch(error => {
                this.setState({ saving: false });
                toastr.error(error);
            });
    }

    redirectToCalendarPage() {
        this.setState({ saving: false });
        this.props.routerActions.push('/calendar');
    }

    handleTextChange = e => {
        const { name, value } = e.currentTarget;
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        recurringEvent[name] = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleParentChange = (e, index, value) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        // Reset the group and type filter if the parent filter is changed.
        if (value !== recurringEvent.eventParentId) {
            recurringEvent.eventGroupId = '';
            recurringEvent.eventTypeId = '';
        }
        recurringEvent.eventParentId = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleGroupChange = (e, index, value) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        // Reset the type filter if the group filter is changed.
        if (value !== recurringEvent.eventGroupId) {
            recurringEvent.eventTypeId = '';
        }
        recurringEvent.eventGroupId = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleTypeChange = (e, index, value) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        recurringEvent.eventTypeId = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleIsAllDayToggle = (e, isChecked) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        if (isChecked) {
            recurringEvent.startTime = null;
            recurringEvent.endTime = null;
        }
        recurringEvent.isAllDay = isChecked;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleDateOrTimeChange = (name, value) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        recurringEvent[name] = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleDateRangeStartChange = (e, date) => this.handleDateOrTimeChange('dateRangeStart', date);

    handleDateRangeEndChange = (e, date) => this.handleDateOrTimeChange('dateRangeEnd', date);

    handleStartTimeChange = (e, time) => this.handleDateOrTimeChange('startTime', time);

    handleEndTimeChange = (e, time) => this.handleDateOrTimeChange('endTime', time);

    handleDaysOfWeekChange = (daysOfWeek) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        recurringEvent.daysOfWeek = daysOfWeek;
        return this.setState({ recurringEvent: recurringEvent });
    }

    handleWeightChange = (e, index, value) => {
        let recurringEvent = Object.assign({}, this.state.recurringEvent);
        recurringEvent.weight = value;
        return this.setState({ recurringEvent: recurringEvent });
    }

    hasRequiredFields() {
        const { recurringEvent } = this.state;

        let missingTimesForNonAllDay = !recurringEvent.isAllDay && (
            recurringEvent.endDate === null ||
            recurringEvent.endTime === null);
        if (recurringEvent.eventParentId === '' ||
            recurringEvent.eventGroupId === '' ||
            recurringEvent.eventTypeId === '' ||
            recurringEvent.name === '' ||
            recurringEvent.dateRangeStart === null ||
            recurringEvent.dateRangeEnd === null ||
            missingTimesForNonAllDay) {
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

    isGroupFilterDisabled = () => this.state.recurringEvent.eventParentId === null || this.state.recurringEvent.eventParentId === '';

    isTypeFilterDisabled = () => this.state.recurringEvent.eventGroupId === null || this.state.recurringEvent.eventGroupId === '';

    render() {
        const { eventParents, eventGroups, eventTypes } = this.props;
        const { recurringEvent, errors, saving } = this.state;
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
                <h1>Recurring Event</h1>
                <form>
                    <ParentSelectField
                        name="eventParentId"
                        eventParents={eventParents}
                        disabled={saving}
                        value={recurringEvent.eventParentId}
                        onChange={this.handleParentChange}
                        errorText={errors.eventParentId}
                    /><br />
                    <GroupSelectField
                        name="eventGroupId"
                        eventGroups={eventGroups}
                        eventParentId={recurringEvent.eventParentId}
                        disabled={this.isGroupFilterDisabled() || saving}
                        value={recurringEvent.eventGroupId}
                        onChange={this.handleGroupChange}
                        errorText={errors.eventGroupId}
                    /><br />
                    <TypeSelectField
                        name="eventTypeId"
                        eventTypes={eventTypes}
                        eventParentId={recurringEvent.eventParentId}
                        eventGroupId={recurringEvent.eventGroupId}
                        disabled={this.isTypeFilterDisabled() || saving}
                        value={recurringEvent.eventTypeId}
                        onChange={this.handleTypeChange}
                        errorText={errors.eventTypeId}
                    /><br />
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={recurringEvent.name}
                        onChange={this.handleTextChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={recurringEvent.description}
                        onChange={this.handleTextChange}
                        errorText={errors.description}
                    /><br />
                    <Toggle
                        name="allDay"
                        label="All day event"
                        labelStyle={toggleLabelStyle}
                        disabled={saving}
                        toggled={recurringEvent.isAllDay}
                        onToggle={this.handleIsAllDayToggle}
                        style={toggleStyle}
                    /><br />
                    <DatePicker
                        name="dateRangeStart"
                        floatingLabelText="Date range start"
                        disabled={saving}
                        value={recurringEvent.dateRangeStart}
                        onChange={this.handleDateRangeStartChange}
                        firstDayOfWeek={0}
                        errorText={errors.dateRangeStart}
                    />
                    <DatePicker
                        name="dateRangeEnd"
                        floatingLabelText="Date range end"
                        disabled={saving}
                        value={recurringEvent.dateRangeEnd}
                        onChange={this.handleDateRangeEndChange}
                        firstDayOfWeek={0}
                        errorText={errors.dateRangeEnd}
                    />
                    {!recurringEvent.isAllDay &&
                        <TimePicker
                            name="startTime"
                            floatingLabelText="Start time"
                            disabled={saving}
                            value={recurringEvent.startTime}
                            onChange={this.handleStartTimeChange}
                            errorText={errors.startTime}
                        />
                    }
                    {!recurringEvent.isAllDay &&
                        <TimePicker
                            name="endTime"
                            floatingLabelText="End time"
                            disabled={saving}
                            value={recurringEvent.endTime}
                            onChange={this.handleEndTimeChange}
                            errorText={errors.endTime}
                        />
                    }
                    <DaysOfWeekCheckboxList
                        disabled={saving}
                        onChange={this.handleDaysOfWeekChange}
                        values={recurringEvent.daysOfWeek}
                        errorText={errors.daysOfWeek}
                    />
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={recurringEvent.weight}
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

RecurringEventPage.propTypes = {
    recurringEvent: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    eventGroups: PropTypes.array.isRequired,
    eventTypes: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    let recurringEvent = {
        eventParentId: '',
        eventGroupId: '',
        eventTypeId: '',
        name: '',
        description: '',
        weight: '',
        startTime: null,
        endTime: null,
        dateRangeStart: null,
        dateRangeEnd: null,
        isAllDay: false,
        daysOfWeek: []
    };

    return {
        recurringEvent,
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

export default connect(mapStateToProps, mapDispatchToProps)(RecurringEventPage);
