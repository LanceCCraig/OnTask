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
import Toggle from 'material-ui/Toggle';

/**
 * Internal dependencies
 */
import * as eventTypeActions from 'ClientApp/actions/eventTypeActions';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import GroupSelectField from 'ClientApp/components/common/groupSelectField';
import { updateEventTypeForDisplay } from 'ClientApp/helpers/generalHelpers';

class EventTypePage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            eventType: updateEventTypeForDisplay(props.eventType),
            errors: {},
            saving: false
        };
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.eventType.id !== nextProps.eventType.id) {
            this.setState({ eventType: updateEventTypeForDisplay(nextProps.eventType) });
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.setState({ saving: true });
        if (this.props.eventType.id === '') {
            this.props.actions.createType(this.state.eventType)
                .then(() => this.redirectToEventTypesPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        } else {
            this.props.actions.updateType(this.state.eventType)
                .then(() => this.redirectToEventTypesPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        }
    }

    redirectToEventTypesPage() {
        this.setState({ saving: false });
        this.props.routerActions.push('/eventTypes');
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        let eventType = Object.assign({}, this.state.eventType);
        eventType[name] = value;
        return this.setState({ eventType: eventType });
    }

    handleToggle = (e, isChecked) => {
        let eventType = Object.assign({}, this.state.eventType);
        eventType[e.currentTarget.name] = isChecked;
        return this.setState({ eventType: eventType });
    }

    handleWeightChange = (event, index, value) => {
        let eventType = Object.assign({}, this.state.eventType);
        eventType.weight = value;
        return this.setState({ eventType: eventType });
    }

    handleParentChange = (event, index, value) => {
        let eventType = Object.assign({}, this.state.eventType);
        // Reset the group filter if the parent filter is changed.
        if (value !== eventType.eventParentId) {
            eventType.eventGroupId = '';
        }
        eventType.eventParentId = value;
        return this.setState({ eventType: eventType });
    }

    handleGroupChange = (event, index, value) => {
        let eventType = Object.assign({}, this.state.eventType);
        eventType.eventGroupId = value;
        return this.setState({ eventType: eventType });
    }

    hasRequiredFields() {
        if (this.state.eventType.name === '' ||
            this.state.eventType.eventParentId === '' ||
            this.state.eventType.eventGroupId === '') {
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

    isGroupFilterDisabled = () => this.state.eventType.eventParentId === null || this.state.eventType.eventParentId === '';

    render() {
        const { eventParents, eventGroups } = this.props;
        const { eventType, errors, saving } = this.state;
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
            <div style={{textAlign: "center"}}>
                <h1>Type</h1>
                <form>
                    <ParentSelectField
                        name="eventParentId"
                        eventParents={eventParents}
                        disabled={saving}
                        value={eventType.eventParentId}
                        onChange={this.handleParentChange}
                        errorText={errors.eventParentId}
                    /><br />
                    <GroupSelectField
                        name="eventGroupId"
                        eventGroups={eventGroups}
                        eventParentId={eventType.eventParentId}
                        disabled={this.isGroupFilterDisabled() || saving}
                        value={eventType.eventGroupId}
                        onChange={this.handleGroupChange}
                        errorText={errors.eventGroupId}
                    /><br />
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={eventType.name}
                        onChange={this.handleChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={eventType.description}
                        onChange={this.handleChange}
                        errorText={errors.description}
                    /><br />
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={eventType.weight}
                        onChange={this.handleWeightChange}
                        errorText={errors.weight}
                    /><br />
                    <Toggle
                        name="isRecommended"
                        label="Is Recommended"
                        labelStyle={toggleLabelStyle}
                        disabled={saving}
                        toggled={eventType.isRecommended}
                        onToggle={this.handleToggle}
                        style={toggleStyle}
                    /><br />
                    <RaisedButton
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        label={saving ? 'Saving...' : 'Save'}
                        labelStyle={{ color: 'white' }}
                        backgroundColor="#2DB1FF"
                        rippleStyle={{ backgroundColor: "#005c93" }}
                        onClick={this.submitForm}
                    />
                </form>
            </div>
        );
    }
}

EventTypePage.propTypes = {
    eventType: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    eventGroups: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function getEventTypeById(eventTypes, id) {
    const eventType = eventTypes.filter(eventType => eventType.id == id);
    return eventType.length ? eventType[0] : null;
}

function mapStateToProps(state, ownProps) {
    const eventTypeId = ownProps.match.params.id;
    let eventType = {
        id: '',
        eventParentId: '',
        eventGroupId: '',
        name: '',
        description: '',
        weight: '',
        isRecommended: false
    };

    if (eventTypeId && state.eventTypes.length > 0) {
        eventType = getEventTypeById(state.eventTypes, eventTypeId);
    }

    return {
        eventType,
        eventParents: state.eventParents,
        eventGroups: state.eventGroups
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventTypeActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventTypePage);
