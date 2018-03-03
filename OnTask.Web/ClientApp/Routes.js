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
import TaskPage from 'ClientApp/pages/taskPage';

const Routes = () => {
    return (
        <Layout>
            <div>
                <Route exact path='/' component={HomePage} />
                <Route path="/counter" component={CounterPage} />
                <Route path="/fetchdata/:startDateIndex?" component={FetchDataPage} />
                <Route path="/calendar" component={CalendarPage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/task" component={TaskPage} />
            </div>
        </Layout>
    );
};

export default Routes;
