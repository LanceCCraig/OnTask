import React from 'React'

class TaskPage extends React.Component {
    constructor(props, context){
        super(props, context);

        this.state = {
            tasks: [
                {
                    taskGroup: '',
                    taskName: '',
                    taskType: '',
                    dueDate: ''
                }
            ]
        }
    }

    render() {
        return (
            <div>
                <div style={{ border: '1px solid', borderRadius: '50px', marginTop: '10%', textAlign: 'center' }}>
                    What group is this task for?
                    <div style={{ margin: 'auto' }}>
                        <input
                            name="taskGroup"
                            value="School"
                            className="btn btn-primary"
                            onClick={this.handleChange()}
                            style={{ margin: '10px 10px 10px 10px' }}
                        />
                        <input
                            name="taskGroup"
                            value="Work"
                            className="btn btn-primary"
                            onClick={this.handleChange()}
                            style={{ margin: '10px 10px 10px 10px' }}
                        />
                        <input
                            name="taskGroup"
                            value="Other"
                            className="btn btn-primary"
                            onClick={this.handleChange()}
                            style={{ margin: '10px 10px 10px 10px' }}
                        />
                    </div>
                </div>
                { console.log(this.state) }
            </div>
        );
    };
}

export default TaskPage;