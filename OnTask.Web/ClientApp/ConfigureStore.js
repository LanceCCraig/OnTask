/**
 * External dependencies
 */
import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import { routerMiddleware } from 'react-router-redux';

/**
 * Internal dependencies
 */
import rootReducer from 'ClientApp/reducers';

export default function configureStore(history, initialState) {
    // Build middleware. These are functions that can process the actions before they reach the store.
    const windowIfDefined = typeof window === 'undefined' ? null : window;
    // If devTools is installed, connect to it
    const devToolsExtension = windowIfDefined && windowIfDefined.devToolsExtension;
    const createStoreWithMiddleware = compose(
        applyMiddleware(thunk, routerMiddleware(history)),
        devToolsExtension ? devToolsExtension() : next => next
    )(createStore);

    const store = createStoreWithMiddleware(rootReducer, initialState);

    // Enable Webpack hot module replacement for reducers
    if (module.hot) {
        // module.hot.accept('./store', () => {
        //     const nextRootReducer = require('./store');
        //     store.replaceReducer(buildRootReducer(nextRootReducer.reducers));
        // });
    }

    return store;
}
