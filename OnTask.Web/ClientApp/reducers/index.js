/**
 * External dependencies
 */
import { combineReducers } from 'redux';
import { routerReducer as router } from 'react-router-redux';

/**
 * Internal dependencies
 */
import auth from 'ClientApp/reducers/authReducer';
import * as counter from 'ClientApp/store/Counter';
import * as weatherForecasts from 'ClientApp/store/WeatherForecasts';

const rootReducer = combineReducers({
    router: router,
    auth: auth,
    counter: counter.reducer,
    weatherForecasts: weatherForecasts.reducer
});

export default rootReducer;