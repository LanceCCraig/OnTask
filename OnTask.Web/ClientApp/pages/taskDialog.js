import React from 'react';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';
import DatePicker from 'material-ui/DatePicker';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';
import '../css/site.css';
import { TextField } from 'material-ui';




/**
 * Dialogs can be nested. This example opens a Date Picker from within a Dialog.
 */
class TaskDialog extends React.Component {
    constructor(props){
        super(props)

        this.state={
            open: false,
            taskCategory: null,
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
        this.setState({open: false});
    };

    handleCreate = () => {
        //post event
        this.setState({open: false});
    };

    handleButtonDisabling = () => {
        console.log(this.state.taskCategory);
        console.log(this.state.taskType);
        console.log(this.state.taskDate);
        if (this.state.taskCategory != null && this.state.taskType != null){
            this.setState({buttonDisabled: false});
        }
    }


    handleCatChange = (e, index, value) => {
        this.setState({taskCategory: value});
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

        function SwitchTaskCategory({taskCategory}) {
            switch(taskCategory) {
                case 'school':
                    return <IfSchool />;
                case 'work': 
                    return <IfWork />;
                case 'personal':
                    return <IfPersonal />;
                // case 'other':
                //     return <IfOther />;
                default:
                    return null;
            }
        };

        const IfSchool = () => (
            <SelectField
            floatingLabelText="School Task Type"
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
        );

        const IfWork = () => (
                <SelectField
                floatingLabelText="Work Task Type"
                value={this.state.taskType}
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                onChange={this.handleTypeChange} >
                    <MenuItem value={null} primaryText="" />
                    <MenuItem value={0} primaryText="Meeting" />
                    <MenuItem value={1} primaryText="Call" />
                    <MenuItem value={2} primaryText="Other Event" />
                </SelectField>
        );

        const IfPersonal = () => (
                <SelectField
                    floatingLabelText="Personal Task Type"
                    value={this.state.taskType}
                    errorText = "*Required field"
                    errorStyle={{color: "#FF8F3A"}}
                    onChange={this.handleTypeChange} >
                        <MenuItem value={null} primaryText="" />
                        <MenuItem value={0} primaryText="Appointment" />
                        <MenuItem value={1} primaryText="Goal" />
                        <MenuItem value={2} primaryText="Other Event" />
                </SelectField>
        );

        // const IfOther = () => (
        //     <TextField
        //         floatingLabelText="Enter Task Type"
        //         value={this.state.event.taskType}
        //         errorText = "*Required field"
        //         errorStyle={{color: "#FF8F3A"}}
        //         onChange={this.handleTypeChange} >
        //     </TextField>
        // );
        

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
                floatingLabelText="Task Category"
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                value={this.state.taskCategory}
                onChange={this.handleCatChange} >
                    <MenuItem value={null} primaryText="" />
                    <MenuItem value='school' primaryText="School" />
                    <MenuItem value='work' primaryText="Work" />
                    <MenuItem value='personal' primaryText="Personal" />
                    <MenuItem value='other' primaryText="Other" />
            </SelectField>
            <SwitchTaskCategory taskCategory={this.state.taskCategory}/>
            <DatePicker 
                errorText = "*Required field"
                errorStyle={{color: "#FF8F3A"}}
                value={this.state.taskDate}
                hintText="Task Date" 
                onChange={this.handleDateChange}/>
            </Dialog>
            </div>
        </div>
        );
    }
}

export default TaskDialog;