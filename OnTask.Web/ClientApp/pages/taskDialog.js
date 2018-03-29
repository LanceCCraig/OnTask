import React from 'react';
import moment from 'moment';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import toastr from 'toastr';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';
import DatePicker from 'material-ui/DatePicker';
import TimePicker from 'material-ui/TimePicker';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';
import Checkbox from 'material-ui/Checkbox';
import '../css/site.css';
import { TextField, NavigationExpandLess } from 'material-ui';


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
            startTime: null,
            endTime: null,
            startDate: null,
            endDate: null,
            frequency: null,
            checked: false,
            buttonDisabled: true
        }
    };

    getCurrentDate() {
        const currentDate = new Date();
        return currentDate;
    };

    checkForValidDate(date) {
        date.setHours(0,0,0,0);
        if (date < this.getCurrentDate().setHours(0,0,0,0)) {
            return false;
        }
        return true;
    }

    checkForValidTime(time) {
        if (time < this.getCurrentDate()) {
            return false;
        }
        return true;
    }

    compareTimes(startTime, endTime) {
        if (endTime < startTime) {
            return false;
        }
        return true;
    }

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
            startTime: null,
            endTime: null,
            startDate: null,
            endDate: null,
            frequency: null,
            checked: false,
            buttonDisabled: true
        });
    };

    handleCreate = () => {
        //post event
        this.setState({open: false});
    };

    handleButtonDisabling = () => {
        console.log("Task Parent: " + this.state.taskParent);
        console.log("Task Parent Index: " + this.state.taskParentIndex);
        console.log("Task Group: " + this.state.taskGroup);
        console.log("Task Group Index: " + this.state.taskGroupIndex);
        console.log("Task Type: " + this.state.taskType);
        console.log("Task Date: " + this.state.taskDate);
        console.log("Start Time: " + this.state.startTime);
        console.log("End Time: " + this.state.endTime);
        console.log("(Recurring) Start Date: " + this.state.startDate);
        console.log("(Recurring) End Date: " + this.state.endDate);
        console.log("(Recurring) Checked: " + this.state.checked);
        if (this.state.taskParent != null && 
            this.state.taskGroup != null && 
            this.state.taskType != null && 
            this.state.taskDate != null &&
            this.state.startTime != null &&
            this.state.endTime != null
        ){
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

    handleTaskDateChange = (event, date) => {
        if (!this.checkForValidDate(date)) {
            window.alert('Date must be on or after today\'s date.')
            return 0;
        }
        
        this.setState({taskDate: date});
        this.handleButtonDisabling();
    }

    handleRecurringStartDateChange = (event, date) => {
        if (!this.checkForValidDate(date)) {
            window.alert('Date must be on or after today\'s date.')
            return 0;
        }

        this.setState({startDate: date});
        this.handleButtonDisabling();
    }

    handleRecurringEndDateChange = (event, date) => {
        if (!this.checkForValidDate(date)) {
            window.alert('Date must be on or after today\'s date.')
            return 0;
        }

        this.setState({endDate: date});
        this.handleButtonDisabling();
    }

    convertToMoment = (time) => {
        let momentTime = moment(time);
        if (this.state.checked) {
            var date = this.state.startDate;
        }
        else {
            var date = this.state.taskDate;
        }
        let momentDate = moment(date);
        let taskTime = moment({
            year: momentDate.year(),
            month: momentDate.month(),
            day: momentDate.date(),
            hour: momentTime.hours(),
            minute: momentTime.minutes()
        });
        if(!(this.checkForValidTime(taskTime._d))) {
            window.alert('Selected time cannot be in the past.');
            return 0;
        }
        return taskTime._d;
    }

    handleStartTimeChange = (event, time, value) => {
        const result = this.convertToMoment(time);
        this.setState({testAppointment: result, startTime: result})
        this.handleButtonDisabling();
    }

    handleEndTimeChange = (event, time) => {
        const result = this.convertToMoment(time);
        if(!(this.compareTimes(this.state.startTime, result))) {
            window.alert('End Time must be greater than Start Time.');
            this.setState({endTime: null});
        }
        else {
            this.setState({testAppointment: result, endTime: result});
            this.handleButtonDisabling();
        }
    }

    updateCheck() {
        this.setState((oldState) => {
            return {
                checked: !(oldState.checked),
                startTime: null,
                endTime: null,
                startDate: null,
                endDate: null
            };
        });
        this.handleButtonDisabling();
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
                                key={eventGroup.id}
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
                    <MenuItem key={null} value={null} primaryText="" />
                    <MenuItem key={0} value={0} primaryText="Paper" />
                    <MenuItem key={1} value={1} primaryText="Project" />
                    <MenuItem key={2} value={2} primaryText="Worksheet" />
                    <MenuItem key={3} value={3} primaryText="Reading" />
                    <MenuItem key={4} value={4} primaryText="Test" />
                    <MenuItem key={5} value={5} primaryText="Other Homework" />
                </SelectField>
            </div>
        );

        function GetRecurringEventFields({checked}) {
            if (checked == true) {
                return <RecurringEventFields />
            }
            else {
                return <div />
            }
        };
        
        const RecurringEventFields = () => (
            <div>
                <DatePicker
                    errorText = "*Required field"
                    errorStyle={{color: "#FF8F3A"}}
                    value={this.state.startDate}
                    hintText="Start Date" 
                    onChange={this.handleRecurringStartDateChange}
                    firstDayOfWeek={0}
                />
                <DatePicker
                    errorText = "*Required field"
                    errorStyle={{color: "#FF8F3A"}}
                    value={this.state.endDate}
                    hintText="End Date" 
                    onChange={this.handleRecurringEndDateChange}
                    firstDayOfWeek={0}
                />
                <TimeFields />           
            </div>
        );

        const TimeFields = () => (
            <div>
                <TimePicker
                    errorText = "*Required field"
                    errorStyle={{color: "#FF8F3A"}}
                    hintText="Start Time"
                    value={this.state.startTime}
                    onChange={this.handleStartTimeChange}/>
                <TimePicker
                    errorText = "*Required field"
                    errorStyle={{color: "#FF8F3A"}}
                    value={this.state.endTime}
                    hintText="End Time"
                    onChange={this.handleEndTimeChange}/>
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
                            key={eventParent.id}
                            value={eventParent.id}
                            primaryText={eventParent.name}
                        />
                    )}
            </SelectField>
            <GetAdditionalFields taskParent={this.state.taskParent}/>
            <Checkbox 
                label="Recurring"
                checked={this.state.checked}
                onCheck={this.updateCheck.bind(this)}
            />
            {(!this.state.checked) ?
                <div>
                    <DatePicker 
                        errorText = "*Required field"
                        errorStyle={{color: "#FF8F3A"}}
                        value={this.state.taskDate}
                        hintText="Task Date" 
                        onChange={this.handleTaskDateChange}
                        firstDayOfWeek={0}/>
                    {(this.state.taskDate == null) ? <div /> :
                        <TimeFields />}
                </div>
                : <div />
            }
            <GetRecurringEventFields checked={this.state.checked}/>
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