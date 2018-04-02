/**
 * External dependencies
 */
import React from 'react';
import { NavLink } from 'react-router-dom';

/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';

const AuthNavLink = () => {
    return (
        authHelper.hasToken() ?
            <NavLink exact to={'/logout'} activeClassName='active'>
                <span className='glyphicon glyphicon-log-out'></span> Logout
            </NavLink> :
            <NavLink exact to={'/login'} activeClassName='active'>
                <span className='glyphicon glyphicon-log-in'></span> Login
            </NavLink>
    );
};

export default AuthNavLink;
