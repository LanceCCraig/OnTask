import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Counter from './components/Counter';
import Calendar from './components/Calendar';

const Routes = () => {
    return (
        <Layout>
            <div>
                <Route exact path='/' component={Home} />
                <Route path="/counter" component={Counter} />
                <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
                <Route path="/calendar" component={Calendar} />
            </div>
        </Layout>
    );
};

export default Routes;
