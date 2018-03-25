/**
 * External dependencies
 */
import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';

/**
 * Internal dependencies
 */
import auth from 'ClientApp/reducers/authReducer';
import eventParents from 'ClientApp/reducers/eventParentReducer';
import * as counter from 'ClientApp/store/Counter';
import * as weatherForecasts from 'ClientApp/store/WeatherForecasts';

const rootReducer = combineReducers({
    router: router,
    auth: auth,
    eventParents: eventParents,
    counter: counter.reducer,
    weatherForecasts: weatherForecasts.reducer
});

export default rootReducer;