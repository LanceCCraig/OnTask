/**
 * External dependencies
 */
import * as React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import { Card, CardActions, CardHeader, CardText } from 'material-ui/Card';
import FlatButton from 'material-ui/FlatButton';
import RaisedButton from 'material-ui/RaisedButton';

/**
 * Internal dependencies
 */
import logo from 'ClientApp/static/logo_banner.png';
import TaskDialog from './taskDialog';

class HomePage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {}
    }
    
    redirectToAddEventPage = () => {
        this.props.routerActions.push('/event');
    }

    redirectToAddRecurringEventPage = () => {
        this.props.routerActions.push('/recurringEvent');
    }

    redirectToParentsPage = () => {
        this.props.routerActions.push('/eventParents');
    }

    redirectToGroupsPage = () => {
        this.props.routerActions.push('/eventGroups');
    }

    redirectToTypesPage = () => {
        this.props.routerActions.push('/eventTypes');
    }

    render() {
        const EventsSummary = () => (
            <Card>
                <CardHeader
                    title="What are Events?"
                    actAsExpander={true}
                    showExpandableButton={true}
                />
                <CardText expandable={true}>
                    <p>Events in OnTask are whatever you want them to be. Homework assignments, quizzes, appointments, goals, or anything else you want to manage in a calendar. Events can be named or categorized in any way you decide. Events are made up of the following components:</p>
                    <ul>
                        <li>Parent</li>
                        <li>Group</li>
                        <li>Type</li>
                        <li>Name/Description</li>
                        <li>Start/End Dates</li>
                        <li>Importance</li>
                    </ul>
                    <p>The best part about this setup? You have full control over all of these. You determine the Parents, Groups, Types, and Names!</p>
                    <p>Need more information about what Parents, Groups, and Types are? See the cards below for more information!</p>
                </CardText>
            </Card>
        );

        const ParentCard = () => (
            <Card>
                <CardHeader
                    title="Parents"
                    actAsExpander={true}
                    showExpandableButton={true}
                />
                <CardText expandable={true}>
                    <p>Parents are the tip of the iceberg. They're the primary areas in your life. As a working college student, maybe your primary focuses are School, Work, and Personal (Goals). Parents can be whatever you want them to be, but these are what each of your Groups and Types will be stored under. You can create Parents (or view the Parents you've created) by clicking the button below!</p>
                    <CardActions>
                        <FlatButton
                            label="Parents"
                            onClick={this.redirectToParentsPage}
                            primary={true}
                        />
                    </CardActions>
                </CardText>
            </Card>
        );

        const GroupCard = () => (
            <Card>
                <CardHeader
                    title="Groups"
                    actAsExpander={true}
                    showExpandableButton={true}
                />
                <CardText expandable={true}>
                    <p>Groups can be considered the various subsections of your parents. These could be your individual classes, or the projects you typically do during work. They're just a collection of subclasses within the Parents. Click the button below to access the Groups page and add some Groups!</p>
                    <CardActions>
                        <FlatButton
                            label="Groups"
                            onClick={this.redirectToGroupsPage}
                            primary={true}
                        />
                    </CardActions>
                </CardText>
            </Card>
        );

        const TypeCard = () => (
            <Card>
                <CardHeader
                    title="Types"
                    actAsExpander={true}
                    showExpandableButton={true}
                />
                <CardText expandable={true}>
                    <p>Types are the individual tasks for each Group within your Parent. These could be your homeworks, quizzes, specific job duties, appointments, etc. To access the Types page, click the button below.</p>
                    <CardActions>
                        <FlatButton
                            label="Types"
                            onClick={this.redirectToTypesPage}
                            primary={true}
                        />
                    </CardActions>
                </CardText>
            </Card>
        );

        return <div className='main-page'>
            <img src={logo} width="250" height="100" alt="OnTask" className='home-logo hidden-xs' />
            <div style={{ height: '1000px', margin: '5px' }} >
                <h3>Welcome to OnTask!</h3><br />
                <p>OnTask is a web-app designed to help you manage your tasks. These tasks are created by you and are displayed in a calendar just for you! You can set up recurring events or single events, and they'll display on your Calendar! If you've never used our app before, check out the cards below for a general overview! Otherwise, click the "New Task" button below to get started!</p><br />
                <RaisedButton
                    label="New Task"
                    onClick={this.redirectToAddEventPage}
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93", margin: 100 }}
                /><br /><br />
                <RaisedButton
                    label="New Recurring Task"
                    onClick={this.redirectToAddRecurringEventPage}
                    labelStyle={{ color: 'white' }}
                    backgroundColor="#2DB1FF"
                    rippleStyle={{ backgroundColor: "#005c93", margin: 100 }}
                /><br /><br />
                <EventsSummary /><br />
                <ParentCard /><br />
                <GroupCard /><br />
                <TypeCard /><br />
            </div>
        </div>;
    }
}

HomePage.propTypes = {
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
    };
}


function mapDispatchToProps(dispatch) {
    return {
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);