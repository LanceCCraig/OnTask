/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import toastr from 'toastr';
import TextField from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';
import SelectField from 'material-ui/SelectField';
import DatePicker from 'material-ui/DatePicker';
import TimePicker from 'material-ui/TimePicker';

/**
 * Internal dependencies
 */
import * as eventActions from 'ClientApp/actions/eventActions';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import GroupSelectField from 'ClientApp/components/common/groupSelectField';
import TypeSelectField from 'ClientApp/components/common/typeSelectField';
import { checkNullEvent } from 'ClientApp/helpers/generalHelpers';
import { Toggle } from 'material-ui';

class EventPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            event: checkNullEvent(props.event),
            isAllDay: false,
            errors: {},
            saving: false
        };
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.event.id !== nextProps.event.id) {
            this.setState({ event: checkNullEvent(nextProps.event) });
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

    handleChange = e => {
        const { name, value } = e.currentTarget;
        let event = Object.assign({}, this.state.event);
        event[name] = value;
        return this.setState({ event: event });
    }

    handleToggle = (e, isChecked) => {
        return this.setState({ isAllDay: isChecked });
    }

    handleWeightChange = (e, index, value) => {
        let event = Object.assign({}, this.state.event);
        event.weight = value;
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

    hasRequiredFields() {
        if (this.state.event.eventParentId === '' ||
            this.state.event.eventGroupId === '' ||
            this.state.event.eventTypeId === '' ||
            this.state.event.name === '' ||
            this.state.event.startDate === '') {
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
        const { event, isAllDay, errors, saving } = this.state;
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
            minHeight: 56
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
                        disabled={saving}
                        value={event.eventGroupId}
                        onChange={this.handleGroupChange}
                        errorText={errors.eventGroupId}
                    /><br />
                    <TypeSelectField
                        name="eventTypeId"
                        eventTypes={eventTypes}
                        eventParentId={event.eventParentId}
                        eventGroupId={event.eventGroupId}
                        disabled={saving}
                        value={event.eventTypeId}
                        onChange={this.handleTypeChange}
                        errorText={errors.eventTypeId}
                    /><br />
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={event.name}
                        onChange={this.handleChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={event.description}
                        onChange={this.handleChange}
                        errorText={errors.description}
                    /><br />
                    {/*<DatePicker
                        name="startDate"
                        floatingLabelText="Start date"
                        disabled={saving}
                        value={event.startDate}*/}
                    <Toggle
                        name="allDay"
                        label="All day event"
                        labelStyle={toggleLabelStyle}
                        disabled={saving}
                        toggled={isAllDay}
                        onToggle={this.handleToggle}
                        style={toggleStyle}
                    /><br />
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={event.weight}
                        onChange={this.handleWeightChange}
                        errorText={errors.weight}
                    /><br />
                </form>
            </div>
        );
    }
}

EventPage.propTypes = {

};

function mapStateToProps(state, ownProps) {

}

function mapDispatchToProps(dispatch) {

}

export default connect(mapStateToProps, mapDispatchToProps)(EventPage);
