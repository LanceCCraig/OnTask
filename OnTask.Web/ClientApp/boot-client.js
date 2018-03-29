/**
 * External dependencies
 */
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import 'bootstrap';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'toastr/build/toastr.min.css';
import 'react-tippy/dist/tippy.css'

/**
 * Internal dependencies
 */
import 'ClientApp/css/site.css';
import configureStore from 'ClientApp/configureStore';
import Routes from 'ClientApp/routes';
import { getAllParents } from 'ClientApp/actions/eventParentActions';
import { getAllGroups } from 'ClientApp/actions/eventGroupActions';
import { getAllTypes } from 'ClientApp/actions/eventTypeActions';
import { getAllEvents } from 'ClientApp/actions/eventActions';
import authHelper from 'ClientApp/helpers/authHelper';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = (window).initialReduxState;
const store = configureStore(history, initialState);
if (authHelper.hasToken()) {
    store.dispatch(getAllParents());
    store.dispatch(getAllGroups());
    store.dispatch(getAllTypes());
    store.dispatch(getAllEvents());
}

function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing configuration
    // and injects the app into a DOM element.
    ReactDOM.render(
        <AppContainer>
            <Provider store={store}>
                <Router history={history}>
                    <Routes/>
                </Router>
            </Provider>
        </AppContainer>,
        document.getElementById('react-app')
    );
}

renderApp();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        //Routes = require('./routes').routes;
        renderApp();
    });
}
