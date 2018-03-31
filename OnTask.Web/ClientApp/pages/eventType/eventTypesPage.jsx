/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import RaisedButton from 'material-ui/RaisedButton';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';

/**
 * Internal dependencies
 */
import * as eventTypeActions from 'ClientApp/actions/eventTypeActions';
import EventTypeList from 'ClientApp/components/eventType/eventTypeList';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import GroupSelectField from 'ClientApp/components/common/groupSelectField';
import { checkNullEventParent } from 'ClientApp/helpers/generalHelpers';

class EventTypesPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            deleteConfirmationOpen: false,
            idToDelete: null,
            eventParentId: null,
            eventGroupId: null
        }
    }

    redirectToAddEventTypePage = () => {
        this.props.routerActions.push('/eventType');
    }

    handleDelete = () => {
        this.props.actions.deleteType(this.state.idToDelete);
        this.handleDeleteConfirmationClose();
    }

    handleDeleteConfirmationOpen = () => {
        this.setState({ deleteConfirmationOpen: true });
    }

    handleDeleteConfirmationClose = () => {
        this.setState({
            deleteConfirmationOpen: false,
            idToDelete: null
        });
    }

    handleMenuOnChange = (event, id) => {
        let type = event.target.innerText;
        if (type === 'Delete') {
            this.setState({ idToDelete: id });
            this.handleDeleteConfirmationOpen();
        }
    }

    handleParentChange = (event, index, value) => {
        // Reset the group filter if the parent filter is changed.
        if (value !== this.state.eventParentId) {
            this.setState({ eventGroupId: null });
        }
        return this.setState({ eventParentId: value });
    }

    handleGroupChange = (event, index, value) => this.setState({eventGroupId: value});

    isGroupFilterDisabled = () => this.state.eventParentId === null || this.state.eventParentId === '';

    render() {
        const { eventTypes, eventGroups, eventParents } = this.props;
        const actions = [
            <FlatButton
                label="No"
                onClick={this.handleDeleteConfirmationClose}
            />,
            <FlatButton
                primary={true}
                label="Yes"
                onClick={this.handleDelete}
            />
        ];
        
        return (
            <div>
                <h1>Types</h1>
                <RaisedButton
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93" }}
                    label="Add Type"
                    onClick={this.redirectToAddEventTypePage}
                /><br />
                <ParentSelectField
                    eventParents={eventParents}
                    disabled={false}
                    onChange={this.handleParentChange}
                    value={this.state.eventParentId}
                />
                <GroupSelectField
                    eventGroups={eventGroups}
                    eventParentId={this.state.eventParentId}
                    disabled={this.isGroupFilterDisabled()}
                    onChange={this.handleGroupChange}
                    value={this.state.eventGroupId}
                />
                <EventTypeList
                    eventTypes={eventTypes}
                    eventParentId={this.state.eventParentId}
                    eventGroupId={this.state.eventGroupId}
                    handleMenuOnChange={this.handleMenuOnChange}
                />
                <Dialog
                    title="Confirm Deletion"
                    actions={actions}
                    modal={false}
                    open={this.state.deleteConfirmationOpen}
                    onRequestClose={this.handleDeleteConfirmationClose}>
                    Are you sure you want to delete this type?
                </Dialog>
            </div>
        );
    }
}

EventTypesPage.propTypes = {
    actions: PropTypes.object.isRequired,
    eventTypes: PropTypes.array.isRequired,
    eventGroups: PropTypes.array.isRequired,
    eventParents: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        eventTypes: state.eventTypes,
        eventGroups: state.eventGroups,
        eventParents: state.eventParents
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventTypeActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventTypesPage);
