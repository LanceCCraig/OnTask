/**
 * External dependencies
 */
import React from 'react';
import { NavLink } from 'react-router-dom';

/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';

const PrivateNavLink = ({ glyphiconClassName, text, ...rest }) => {
    return (
        authHelper.hasToken()
            ? <NavLink activeClassName="active" {...rest}><span className={glyphiconClassName}></span>{text}</NavLink>
            : null
    );
};

export default PrivateNavLink;
