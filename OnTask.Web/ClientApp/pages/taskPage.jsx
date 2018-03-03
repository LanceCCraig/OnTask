import React from 'React'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

class TaskPage extends React.Component {
    constructor(props, context){
        super(props, context);

        this.state = {
            tasks: [
                {
                    taskName: '',
                    dueDate: '',
                    taskType: ''
                }
            ]
        }
    }

    render() {
        return (
            <MuiThemeProvider>
                <div>
                    Hello World!
                </div>
            </MuiThemeProvider>
        );
    }
}

export default TaskPage;