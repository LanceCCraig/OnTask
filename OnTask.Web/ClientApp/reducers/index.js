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
import eventGroups from 'ClientApp/reducers/eventGroupReducer';
import eventTypes from 'ClientApp/reducers/eventTypeReducer';
import events from 'ClientApp/reducers/eventReducer';
import recommendations from 'ClientApp/reducers/recommendationReducer';
import * as counter from 'ClientApp/store/Counter';
import * as weatherForecasts from 'ClientApp/store/WeatherForecasts';

const rootReducer = combineReducers({
    router: router,
    auth: auth,
    eventParents: eventParents,
    eventGroups: eventGroups,
    eventTypes: eventTypes,
    events: events,
    recommendations: recommendations,
    counter: counter.reducer,
    weatherForecasts: weatherForecasts.reducer
});

export default rootReducer;
