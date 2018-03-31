/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import RaisedButton from 'material-ui/RaisedButton';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';

/**
 * Internal dependencies
 */
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import EventParentList from 'ClientApp/components/eventParent/eventParentList';

class EventParentsPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            deleteConfirmationOpen: false,
            idToDelete: null
        }
    }

    redirectToAddEventParentPage = () => {
        this.props.routerActions.push('/eventParent');
    }

    handleDelete = () => {
        this.props.actions.deleteParent(this.state.idToDelete);
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

    render() {
        const { eventParents } = this.props;
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
                <h1>Parents</h1>
                <RaisedButton
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93" }}
                    label="Add Parent"
                    onClick={this.redirectToAddEventParentPage}
                />
                <EventParentList
                    eventParents={eventParents}
                    handleMenuOnChange={this.handleMenuOnChange}
                />
                <Dialog
                    title="Confirm Deletion"
                    actions={actions}
                    modal={false}
                    open={this.state.deleteConfirmationOpen}
                    onRequestClose={this.handleDeleteConfirmationClose}>
                    Are you sure you want to delete this parent?
                </Dialog>
            </div>
        );
    }
}

EventParentsPage.propTypes = {
    actions: PropTypes.object.isRequired,
    eventParents: PropTypes.array.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        eventParents: state.eventParents
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventParentsPage);
