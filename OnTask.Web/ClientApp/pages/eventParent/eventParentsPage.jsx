/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import RaisedButton from 'material-ui/RaisedButton';

/**
 * Internal dependencies
 */
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import EventParentList from 'ClientApp/components/eventParent/eventParentList';

class EventParentsPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            selectedIds: []
        }
    }

    deleteSelected = () => {
        for (let i = 0, len = this.state.selectedIds.length; i < len; i++) {
            this.props.actions.deleteParent(this.state.selectedIds[i]);
        }
        this.setState({ selectedIds: [] });
    }

    redirectToAddEventParentPage = () => {
        this.props.routerActions.push('/eventParent');
    }

    handleRowSelection = (selectedRowIndices) => {
        let newSelectedIds = selectedRowIndices.map(i => {
            return this.props.eventParents[i].id;
        });
        this.setState({ selectedIds: newSelectedIds });
    }

    render() {
        const { eventParents } = this.props;
        const { selectedIds } = this.state;
        
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
                <RaisedButton
                    secondary
                    label={selectedIds.length && selectedIds.length > 1 ? "Delete Parents" : "Delete Parent"}
                    className={selectedIds.length ? "" : "hidden"}
                    onClick={this.deleteSelected}
                />
                <EventParentList
                    eventParents={eventParents}
                    handleRowSelection={this.handleRowSelection}
                    selectedIds={selectedIds}
                />
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
