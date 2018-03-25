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

/**
 * Internal dependencies
 */
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import WeightSelectField from 'ClientApp/components/common/weightSelectField';
import { checkNullEventParent } from 'ClientApp/helpers/generalHelpers';

class EventParentPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            eventParent: checkNullEventParent(props.eventParent),
            errors: {},
            saving: false
        }
    }

    componentWillReceiveProps(nextProps) {
        if (this.props.eventParent.id !== nextProps.eventParent.id) {
            this.setState({ eventParent: checkNullEventParent(nextProps.eventParent) });
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.setState({ saving: true });
        if (this.props.eventParent.id === '') {
            this.props.actions.createParent(this.state.eventParent)
                .then(() => this.redirectToEventParentsPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        } else {
            this.props.actions.updateParent(this.state.eventParent)
                .then(() => this.redirectToEventParentsPage())
                .catch(error => {
                    this.setState({ saving: false });
                    toastr.error(error);
                });
        }
    }

    redirectToEventParentsPage() {
        this.setState({ saving: false });
        this.props.routerActions.push('/eventParents');
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        let eventParent = Object.assign({}, this.state.eventParent);
        eventParent[name] = value;
        return this.setState({ eventParent: eventParent });
    }

    handleWeightChange = (event, index, value) => {
        let eventParent = Object.assign({}, this.state.eventParent);
        eventParent.weight = value;
        return this.setState({ eventParent: eventParent });
    }

    hasRequiredFields() {
        if (this.state.eventParent.name === '') {
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
        const { eventParent, errors, saving } = this.state;
        return (
            <div style={{textAlign: "center"}}>
                <h1>Parent</h1>
                <form>
                    <TextField
                        name="name"
                        floatingLabelText="Name"
                        disabled={saving}
                        value={eventParent.name}
                        onChange={this.handleChange}
                        errorText={errors.name}
                    /><br />
                    <TextField
                        name="description"
                        floatingLabelText="Description"
                        disabled={saving}
                        value={eventParent.description}
                        onChange={this.handleChange}
                        errorText={errors.description}
                    /><br />
                    <WeightSelectField
                        name="weight"
                        disabled={saving}
                        value={eventParent.weight}
                        onChange={this.handleWeightChange}
                        errorText={errors.weight}
                    /><br/>
                    <RaisedButton
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        primary
                        label={saving ? 'Saving...' : 'Save'}
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
//    value={eventParent.weight}
//    onChange={this.handleChange}
//    errorText={errors.weight}
///> <br />

EventParentPage.propTypes = {
    eventParent: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired,
    routerActions: PropTypes.object.isRequired
};

function getEventParentById(eventParents, id) {
    const eventParent = eventParents.filter(eventParent => eventParent.id == id);
    return eventParent.length ? eventParent[0] : null;
}

function mapStateToProps(state, ownProps) {
    const eventParentId = ownProps.match.params.id;
    let eventParent = {
        id: '',
        name: '',
        description: '',
        weight: ''
    };

    if (eventParentId && state.eventParents.length > 0) {
        eventParent = getEventParentById(state.eventParents, eventParentId);
    }

    return {
        eventParent: eventParent
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventParentPage);
