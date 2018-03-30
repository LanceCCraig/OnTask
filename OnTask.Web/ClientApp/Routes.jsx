/**
 * External dependencies
 */
import * as React from 'react';
import { Route } from 'react-router-dom';

/**
 * Internal dependencies
 */
import PrivateRoute from 'ClientApp/components/privateRoute';
import Layout from 'ClientApp/components/layout';
import HomePage from 'ClientApp/pages/homePage';
import ForgotPasswordPage from 'ClientApp/pages/auth/forgotPasswordPage';
import RegisterPage from 'ClientApp/pages/auth/registerPage';
import LoginPage from 'ClientApp/pages/auth/loginPage';
import LogoutPage from 'ClientApp/pages/auth/logoutPage';
import FetchDataPage from 'ClientApp/pages/fetchDataPage';
import CounterPage from 'ClientApp/pages/counterPage';
import CalendarPage from 'ClientApp/pages/calendarPage';
import EventParentsPage from 'ClientApp/pages/eventParent/eventParentsPage';
import EventParentPage from 'ClientApp/pages/eventParent/eventParentPage';
import EventGroupsPage from 'ClientApp/pages/eventGroup/eventGroupsPage';
import EventGroupPage from 'ClientApp/pages/eventGroup/eventGroupPage';
import EventTypesPage from 'ClientApp/pages/eventType/eventTypesPage';
import EventTypePage from 'ClientApp/pages/eventType/eventTypePage';
import EventPage from 'ClientApp/pages/event/eventPage';
import RecurringEventPage from 'ClientApp/pages/event/recurringEventPage';

const Routes = () => {
    return (
        <Layout>
            <div>
                <PrivateRoute exact path='/' component={HomePage} />
                <PrivateRoute path="/eventParents" component={EventParentsPage} />
                <PrivateRoute exact path="/eventParent" component={EventParentPage} />
                <PrivateRoute path="/eventParent/:id" component={EventParentPage} />
                <PrivateRoute path="/eventGroups" component={EventGroupsPage} />
                <PrivateRoute exact path="/eventGroup" component={EventGroupPage} />
                <PrivateRoute path="/eventGroup/:id" component={EventGroupPage} />
                <PrivateRoute path="/eventTypes" component={EventTypesPage} />
                <PrivateRoute exact path="/eventType" component={EventTypePage} />
                <PrivateRoute path="/eventType/:id" component={EventTypePage} />
                <PrivateRoute path="/event" component={EventPage} />
                <PrivateRoute path="/recurringEvent" component={RecurringEventPage} />
                <Route path="/counter" component={CounterPage} />
                <Route path="/fetchdata/:startDateIndex?" component={FetchDataPage} />
                <PrivateRoute path="/calendar" component={CalendarPage} />
                <Route path="/register" component={RegisterPage} />
                <Route path="/forgotPassword" component={ForgotPasswordPage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/logout" component={LogoutPage} />
            </div>
        </Layout>
    );
};

export default Routes;
