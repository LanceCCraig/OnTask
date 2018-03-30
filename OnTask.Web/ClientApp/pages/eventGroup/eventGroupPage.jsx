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

/**
 * Internal dependencies
 */
import * as eventGroupActions from 'ClientApp/actions/eventGroupActions';
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import { updateEventGroupForDisplay } from 'ClientApp/helpers/generalHelpers';

class EventGroupPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            eventGroup: updateEventGroupForDisplay(props.eventGroup),
            errors: {},
            saving: false
        }
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.eventGroup.id !== nextProps.eventGroup.id) {
            this.setState({ eventGroup: updateEventGroupForDisplay(nextProps.eventGroup) });
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.setState({ saving: true });
        if (this.props.eventGroup.id === '') {
            this.props.actions.createGroup(this.state.eventGroup)
                .then(() => this.redirectToEventGroupsPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        } else {
            this.props.actions.updateGroup(this.state.eventGroup)
                .then(() => this.redirectToEventGroupsPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        }
    }

    redirectToEventGroupsPage() {
        this.setState({ saving: false });
        this.props.routerActions.push('/eventGroups');
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        let eventGroup = Object.assign({}, this.state.eventGroup);
        eventGroup[name] = value;
        return this.setState({ eventGroup: eventGroup });
    }

    handleWeightChange = (event, index, value) => {
        let eventGroup = Object.assign({}, this.state.eventGroup);
        eventGroup.weight = value;
        return this.setState({ eventGroup: eventGroup });
    }

    handleParentChange = (event, index, value) => {
        let eventGroup = Object.assign({}, this.state.eventGroup);
        eventGroup.eventParentId = value;
        return this.setState({ eventGroup: eventGroup });
    }

    hasRequiredFields() {
        if (this.state.eventGroup.name === '' || this.state.eventGroup.eventParentId == '') {
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

    render() {
        const { eventParents } = this.props;
        const { eventGroup, errors, saving } = this.state;
        return (
            <div style={{textAlign: "center"}}>
                <h1>Group</h1>
                <form>
                    <ParentSelectField
                        name="eventParentId"
                        eventParents={eventParents}
                        disabled={saving}
                        value={eventGroup.eventParentId}
                        onChange={this.handleParentChange}
                        errorText={errors.eventParentId}
                    /><br />
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={eventGroup.name}
                        onChange={this.handleChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={eventGroup.description}
                        onChange={this.handleChange}
                        errorText={errors.description}
                    /><br />
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={eventGroup.weight}
                        onChange={this.handleWeightChange}
                        errorText={errors.weight}
                    /><br/>
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
//<TextField
//    name="weight"
//    floatingLabelText="Weight"
//    disabled={saving}
//    value={eventGroup.weight}
//    onChange={this.handleChange}
//    errorText={errors.weight}
///> <br />

EventGroupPage.propTypes = {
    eventGroup: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired,
    parentActions: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function getEventGroupById(eventGroups, id) {
    const eventGroup = eventGroups.filter(eventGroup => eventGroup.id == id);
    return eventGroup.length ? eventGroup[0] : null;
}

function mapStateToProps(state, ownProps) {
    const eventGroupId = ownProps.match.params.id;
    let eventGroup = {
        id: '',
        eventParentId: '',
        name: '',
        description: '',
        weight: ''
    };

    if (eventGroupId && state.eventGroups.length > 0) {
        eventGroup = getEventGroupById(state.eventGroups, eventGroupId);
    }

    return {
        eventGroup: eventGroup,
        eventParents: state.eventParents
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventGroupActions, dispatch),
        parentActions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventGroupPage);
