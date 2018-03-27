/**
 * External dependencies
 */
import * as React from 'react';
import { Route } from 'react-router-dom';

/**
 * Internal dependencies
 */
import Layout from 'ClientApp/components/layout';
import HomePage from 'ClientApp/pages/homePage';
import FetchDataPage from 'ClientApp/pages/fetchDataPage';
import CounterPage from 'ClientApp/pages/counterPage';
import CalendarPage from 'ClientApp/pages/calendarPage';
import LoginPage from 'ClientApp/pages/auth/loginPage';
import EventParentsPage from 'ClientApp/pages/eventParent/eventParentsPage';
import EventParentPage from 'ClientApp/pages/eventParent/eventParentPage';
import EventGroupsPage from 'ClientApp/pages/eventGroup/eventGroupsPage';
import EventGroupPage from 'ClientApp/pages/eventGroup/eventGroupPage';

const Routes = () => {
    return (
        <Layout>
            <div>
                <Route exact path='/' component={HomePage} />
                <Route path="/eventParents" component={EventParentsPage} />
                <Route exact path="/eventParent" component={EventParentPage} />
                <Route path="/eventParent/:id" component={EventParentPage} />
                <Route path="/eventGroups" component={EventGroupsPage} />
                <Route exact path="/eventGroup" component={EventGroupPage} />
                <Route path="/eventGroup/:id" component={EventGroupPage} />
                <Route path="/counter" component={CounterPage} />
                <Route path="/fetchdata/:startDateIndex?" component={FetchDataPage} />
                <Route path="/calendar" component={CalendarPage} />
                <Route path="/login" component={LoginPage} />
            </div>
        </Layout>
    );
};

export default Routes;
