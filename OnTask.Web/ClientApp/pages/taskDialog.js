import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import toastr from 'toastr';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';
import DatePicker from 'material-ui/DatePicker';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';
import '../css/site.css';
import { TextField } from 'material-ui';


/**
* Internal dependencies
*/
import * as eventParentActions from 'ClientApp/actions/eventParentActions';
import * as eventGroupActions from 'ClientApp/actions/eventGroupActions';
import EventApi from 'ClientApp/api/eventApi';


/**
 * Dialogs can be nested. This example opens a Date Picker from within a Dialog.
 */
class TaskDialog extends React.Component {
    constructor(props){
        super(props)

        this.state={
            open: false,
            taskParent: null,
            taskParentIndex: null,
            taskGroup: null,
            taskGroupIndex: null,
            taskType: null,
            taskDate: null,
            // taskPriority: null,
            buttonDisabled: true,
        }
    };

    getCurrentDate() {
        const currentDate = new Date();
        currentDate.setHours(0,0,0,0)
        return currentDate;
    };

    handleOpen = () => {
        this.setState({open: true});
    };

    handleCancel = () => {
        this.setState({
            open: false,
            taskParent: null,
            taskParentIndex: null,
            taskGroup: null,
            taskGroupIndex: null,
            taskType: null,
            taskDate: null,
            buttonDisabled: true
        });
    };

    handleCreate = () => {
        //post event
        this.setState({open: false});
    };

    handleButtonDisabling = () => {
        console.log(this.state.taskParent);
        console.log(this.state.taskParentIndex);
        console.log(this.state.taskGroup);
        console.log(this.state.taskGroupIndex);
        console.log(this.state.taskType);
        console.log(this.state.taskDate);
        if (this.state.taskParent != null && this.state.taskGroup != null && this.state.taskType != null && this.state.taskDate != null){
            this.setState({buttonDisabled: false});
        }
    }

    handleParentChange = (e, index, value) => {
        this.setState({taskParent: value,
            taskParentIndex: index});
        this.handleButtonDisabling();
    }

    handleGroupChange = (e, index, value) => {
        this.setState({taskGroup: value,
            taskGroupIndex: index});
        this.handleButtonDisabling();
    }
    handleTypeChange = (e, index, value) => {
        this.setState({taskType: value});
        this.handleButtonDisabling();
    }
    handleDateChange = (event, date) => {
        date.setHours(0,0,0,0);
        if (date < this.getCurrentDate()) {
            window.alert('Date must be on or after today\'s date.')
        }
        else {
            this.setState({taskDate: date});
            this.handleButtonDisabling();
        }
    }

    render() {
        const { eventParents } = this.props;
        const { eventGroups } = this.props;
        const actions = [
        <RaisedButton
            label="Cancel"
            style={{margin: '5px'}}
            keyboardFocused={false}
            labelStyle={{ color: '#FF8F3A' }}
            onClick={this.handleCancel}
        />,
        <RaisedButton
            label="Create Event"
            disabled={this.state.buttonDisabled}
            style={{margin: '5px'}}
            labelStyle={{ color: 'white' }}
            backgroundColor="#2DB1FF"
            keyboardFocused={false}
            onClick={this.handleCreate}
        />,
        ];

        function GetAdditionalFields({taskParent}) {
            if (taskParent != null) {
                return <AdditionalFields />;
            }
            else {
                return <div />
            }
        };

        const AdditionalFields = () => (
            <div>
                <SelectField
                floatingLabelText="Task Group"
                value={this.state.taskGroup}
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                onChange={this.handleGroupChange} >
                    {eventGroups.filter(eventGroup => 
                        eventGroup.eventParentId === this.state.taskParent).map(eventGroup =>
                            <MenuItem
                                value={eventGroup.id}
                                primaryText={eventGroup.name}
                            />
                    )}
                </SelectField>
                
                <SelectField
                floatingLabelText="Task Type"
                value={this.state.taskType}
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                onChange={this.handleTypeChange} >
                    <MenuItem value={null} primaryText="" />
                    <MenuItem value={0} primaryText="Paper" />
                    <MenuItem value={1} primaryText="Project" />
                    <MenuItem value={2} primaryText="Worksheet" />
                    <MenuItem value={3} primaryText="Reading" />
                    <MenuItem value={4} primaryText="Test" />
                    <MenuItem value={5} primaryText="Other Homework" />
                </SelectField>
            </div>
        );      

        return (
        <div>
            <RaisedButton 
                label="New Task" 
                onClick={this.handleOpen}
                labelStyle={{ color: 'white' }}
                backgroundColor="#2DB1FF"
                rippleStyle={{backgroundColor: "#005c93"}}
                // hoverColor="#0092e8" 
                />
            <div style={{ margin: 'auto', maxWidth: '400px' }}>
            <Dialog
            title="Create New Task"
            overlayClassName="newTaskOverlay"
            style={{
                maxWidth: '400px', 
                margin: 'auto', 
                position: 'absolute', 
                top: '50px', 
                display: !this.state.open ? 'none' : '',
                left: '0',
                padding: '10px',
                right: '0'}}
            actions={actions}
            paperProps={{style: {margin: 'auto'}}}
            modal={false}
            open={this.state.open}
            onRequestClose={this.handleClose}
            autoScrollBodyContent>
            <SelectField
                floatingLabelText="Task Parent"
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                value={this.state.taskParent}
                onChange={this.handleParentChange} >
                    {eventParents.map(eventParent =>
                        <MenuItem
                            value={eventParent.id}
                            primaryText={eventParent.name}
                        />
                    )}
            </SelectField>
            <GetAdditionalFields taskParent={this.state.taskParent}/>
            <DatePicker 
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                value={this.state.taskDate}
                hintText="Task Date" 
                onChange={this.handleDateChange}
                firstDayOfWeek={0}/>
            </Dialog>
            </div>
        </div>
        );
    }
}

TaskDialog.propTypes= {
    eventParents: PropTypes.array.isRequired,
    eventGroups: PropTypes.array.isRequired,
    actions: PropTypes.object.isRequired,
    parentActions: PropTypes.object.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        eventParents: state.eventParents,
        eventGroups: state.eventGroups,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(eventGroupActions, dispatch),
        parentActions: bindActionCreators(eventParentActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(TaskDialog);