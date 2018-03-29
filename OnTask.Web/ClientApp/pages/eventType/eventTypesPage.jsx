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
import * as eventGroupActions from 'ClientApp/actions/eventGroupActions';
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
//import EventTypeList from 'ClientApp/components/eventType/eventTypeList';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';
import { checkNullEventParent } from 'ClientApp/helpers/generalHelpers';

class EventTypesPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            deleteConfirmationOpen: false,
            idToDelete: null,
            eventParentId: null,
            eventParentIndex: null,
            eventGroupId: null,
            eventGroupIndex: null
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
        return this.setState({
            eventParentId: value,
            eventParentIndex: index
        });
    }

    handleGroupChange = (event, index, value) => {
        return this.setState({
            eventGroupId: value,
            eventGroupIndex: index
        });
    }

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
                keyboardFocused={true}
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
                <SelectField
                    name="eventParent"
                    floatingLabelText="Parent"
                    disabled={false}
                    value={this.state.eventParentId}
                    onChange={this.handleParentChange} >
                    <MenuItem value={null} primaryText="" />
                    {eventParents.map(eventParent =>
                        <MenuItem key={eventParent.id} value={eventParent.id} primaryText={eventParent.name} />
                    )}<br />
                </SelectField>
                <SelectField
                    name="eventGroup"
                    floatingLabelText="Group"
                    disabled={false}
                    value={this.state.eventGroupId}
                    onChange={this.handleGroupChange} >
                    <MenuItem value={null} primaryText="" />
                    {eventGroups.filter(eventGroup => {
                        eventGroup.eventParentId == this.state.eventParent.id;
                    }).map(eventGroup =>
                        <MenuItem key={eventGroup.id} value={eventGroup.id} primaryText={eventGroup.name} />
                    )}<br />
                </SelectField>
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
    groupActions: PropTypes.object.isRequired,
    eventGroups: PropTypes.array.isRequired,
    parentActions: PropTypes.object.isRequired,
    eventParentId: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    let eventParent = {
        id: '',
        name: '',
        description: '',
        weight: ''
    };

    return {
        eventTypes: state.eventTypes,
        eventGroups: state.eventGroups,
        eventParents: state.eventParents,
        eventParent: eventParent
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventTypeActions, dispatch),
        groupActions: bindActionCreators(eventGroupActions, dispatch),
        parentActions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventTypesPage);
