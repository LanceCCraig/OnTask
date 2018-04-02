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
import * as eventGroupActions from 'ClientApp/actions/eventGroupActions';
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import EventGroupList from 'ClientApp/components/eventGroup/eventGroupList';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import { checkNullEventParent } from 'ClientApp/helpers/generalHelpers';

class EventGroupsPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            deleteConfirmationOpen: false,
            idToDelete: null,
            eventParentId: null
        }
    }

    redirectToAddEventGroupPage = () => {
        this.props.routerActions.push('/eventGroup');
    }

    handleDelete = () => {
        this.props.actions.deleteGroup(this.state.idToDelete);
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

    handleParentChange = (event, index, value) => this.setState({ eventParentId: value });

    render() {
        const { eventGroups, eventParents } = this.props;
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
                <h1>Groups</h1>
                <RaisedButton
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93" }}
                    label="Add Group"
                    onClick={this.redirectToAddEventGroupPage}
                /><br />
                <ParentSelectField
                    eventParents={eventParents}
                    disabled={false}
                    onChange={this.handleParentChange}
                    value={this.state.eventParentId}
                />
                <EventGroupList
                    eventGroups={eventGroups}
                    eventParentId={this.state.eventParentId}
                    handleMenuOnChange={this.handleMenuOnChange}
                />
                <Dialog
                    title="Confirm Deletion"
                    actions={actions}
                    modal={false}
                    open={this.state.deleteConfirmationOpen}
                    onRequestClose={this.handleDeleteConfirmationClose}>
                    Are you sure you want to delete this group?
                </Dialog>
            </div>
        );
    }
}

EventGroupsPage.propTypes = {
    actions: PropTypes.object.isRequired,
    eventGroups: PropTypes.array.isRequired,
    eventParents: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        eventGroups: state.eventGroups,
        eventParents: state.eventParents
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventGroupActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventGroupsPage);
