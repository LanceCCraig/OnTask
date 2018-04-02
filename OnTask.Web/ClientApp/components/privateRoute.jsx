/**
 * External dependencies
 */
import React from 'react';
import { Route, Redirect } from 'react-router-dom';

/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';

const PrivateRoute = ({ component, ...rest }) => {
    return (
        authHelper.hasToken()
            ? <Route {...rest} component={component} />
            : <Route {...rest} render={props => (
                <Redirect to={{ pathname: '/login', state: {from: props.location } }} />
            )} />
    );
};

export default PrivateRoute;
