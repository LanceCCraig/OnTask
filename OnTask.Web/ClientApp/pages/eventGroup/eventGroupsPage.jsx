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

/**
 * Internal dependencies
 */
import * as eventGroupActions from 'ClientApp/actions/eventGroupActions';
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import EventGroupList from 'ClientApp/components/eventGroup/eventGroupList';
import ParentSelectField from 'ClientApp/components/common/parentSelectField';

class EventGroupsPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            selectedIds: [],
            curParentId: null,
            curParentIndex: null
        }
    }

    deleteSelected = () => {
        for (let i = 0, len = this.state.selectedIds.length; i < len; i++) {
            this.props.actions.deleteGroup(this.state.selectedIds[i]);
        }
        this.setState({ selectedIds: [] });
    }

    redirectToAddEventGroupPage = () => {
        this.props.routerActions.push('/eventGroup');
    }

    handleRowSelection = (selectedRowIndices) => {
        let newSelectedIds = selectedRowIndices.map(i => {
            return this.props.eventGroups[i].id;
        });
        this.setState({ selectedIds: newSelectedIds });
    }

    handleParentChange = (event, index, value) => {
        return this.setState({
            curParentId: value,
            curParentIndex: index
        });
    }

    render() {
        const { eventGroups, eventParents } = this.props;
        const { selectedIds } = this.state;
        
        return (
            <div>
                <h1>Groups</h1>
                <RaisedButton
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93" }}
                    label="Add Group"
                    onClick={this.redirectToAddEventGroupPage}
                />
                <RaisedButton
                    secondary
                    label={selectedIds.length && selectedIds.length > 1 ? "Delete Groups" : "Delete Group"}
                    className={selectedIds.length ? "" : "hidden"}
                    onClick={this.deleteSelected}
                /> <br />
                <SelectField
                    name="eventParentId"
                    floatingLabelText="Parent"
                    disabled={false}
                    value={this.state.curParentId}
                    onChange={this.handleParentChange} >
                    <MenuItem value={null} primaryText="" />
                    {eventParents.map(eventParent =>
                        <MenuItem value={eventParent.id} primaryText={eventParent.name} />
                    )}<br />
                </SelectField>
                <EventGroupList
                    eventGroups={eventGroups}
                    eventParentId={this.state.curParentId}
                    handleRowSelection={this.handleRowSelection}
                    selectedIds={selectedIds}
                />
            </div>
        );
    }
}

EventGroupsPage.propTypes = {
    actions: PropTypes.object.isRequired,
    eventGroups: PropTypes.array.isRequired,
    parentActions: PropTypes.object.isRequired,
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
        parentActions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(EventGroupsPage);
