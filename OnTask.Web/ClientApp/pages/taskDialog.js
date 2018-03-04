// import React from 'React'

// class TaskDialog extends React.Component {
//     constructor(props, context){
//         super(props, context);

//         this.state = {
//             tasks: [
//                 {
//                     taskGroup: '',
//                     taskName: '',
//                     taskCategory: '',
//                     dueDate: ''
//                 }
//             ]
//         }
//     }

//     render() {
//         return (
//             <div>
//                 <div style={{ border: '1px solid', borderRadius: '50px', marginTop: '10%', textAlign: 'center' }}>
//                     What group is this task for?
//                     <div style={{ margin: 'auto' }}>
//                         <input
//                             name="taskGroup"
//                             value="School"
//                             className="btn btn-primary"
//                             // onClick={this.handleChange()}
//                             style={{ margin: '10px 10px 10px 10px' }}
//                         />
//                         <input
//                             name="taskGroup"
//                             value="Work"
//                             className="btn btn-primary"
//                             // onClick={this.handleChange()}
//                             style={{ margin: '10px 10px 10px 10px' }}
//                         />
//                         <input
//                             name="taskGroup"
//                             value="Other"
//                             className="btn btn-primary"
//                             // onClick={this.handleChange()}
//                             style={{ margin: '10px 10px 10px 10px' }}
//                         />
//                     </div>
//                 </div>
//                 { console.log(this.state) }
//             </div>
//         );
//     };
// }

// export default TaskDialog;




import React from 'react';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';
import DatePicker from 'material-ui/DatePicker';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';
import '../css/site.css';


/**
 * Dialogs can be nested. This example opens a Date Picker from within a Dialog.
 */
class TaskDialog extends React.Component {
  state = {
    open: false,
    taskCategory: 'school',
    taskType: 1,
  };


  handleOpen = () => {
    this.setState({open: true});
  };

  handleClose = () => {
    this.setState({open: false});
  };

  handleCatChange = (event, index, value) => this.setState({taskCategory: value});
  handleTypeChange = (event, index, value) => this.setState({taskType: value});

  render() {

    

    const actions = [
      <RaisedButton
        label="Ok"
        primary={true}
        keyboardFocused={false}
        onClick={this.handleClose}
      />,
    ];

    const IfSchool = () => (
        <SelectField
        floatingLabelText="School Task Type"
        value={this.state.taskType}
        onChange={this.handleTypeChange} >
            <MenuItem value={1} primaryText="Paper" />
            <MenuItem value={2} primaryText="Project" />
            <MenuItem value={3} primaryText="Worksheet" />
            <MenuItem value={4} primaryText="Reading" />
            <MenuItem value={5} primaryText="Test" />
            <MenuItem value={6} primaryText="Other Homework" />
        </SelectField>
    );

    const IfWork = () => (
            <SelectField
            floatingLabelText="Work Task Type"
            value={this.state.taskType}
            onChange={this.handleTypeChange} >
                <MenuItem value={1} primaryText="Meeting" />
                <MenuItem value={2} primaryText="Call" />
                <MenuItem value={3} primaryText="Other Event" />
            </SelectField>
    );

    const IfPersonal = () => (
            <SelectField
                floatingLabelText="Personal Task Type"
                value={this.state.taskType}
                onChange={this.handleTypeChange} >
                    <MenuItem value={1} primaryText="Appointment" />
                    <MenuItem value={2} primaryText="Goal" />
                    <MenuItem value={3} primaryText="Other Event" />
            </SelectField>
    );



    function SwitchTaskCategory({taskCategory}) {
        switch(taskCategory) {
            case 'school':
                return <IfSchool />;
            case 'work': 
                return <IfWork />;
            case 'personal':
                return <IfPersonal />;
            default:
                return null;
        }
    };

    return (
      <div>
        <RaisedButton 
            label="New Task" 
            onClick={this.handleOpen} 
            primary />
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
            value={this.state.taskCategory}
            onChange={this.handleCatChange} >
                <MenuItem value='school' primaryText="School" />
                <MenuItem value='work' primaryText="Work" />
                <MenuItem value='personal' primaryText="Personal" />
        </SelectField>
        <div><SwitchTaskCategory taskCategory={this.state.taskCategory}/></div>
        <DatePicker hintText="Due Date" />
        </Dialog>
        </div>
      </div>
    );
  }
}

export default TaskDialog;