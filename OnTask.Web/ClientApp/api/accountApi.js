/**
 * External dependencies
 */
import 'isomorphic-fetch';

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';
import { handleResponse, handleError } from 'ClientApp/helpers/apiHelpers';
import authHelper from 'ClientApp/helpers/authHelper';

let AccountApi = {
    login(email, password) {
        const requestOptions = {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify({
                email: email,
                password: password
            })
        };
        
        return fetch(`${Constants.ACCOUNT_API_URL}/Login`, requestOptions)
            .then(handleResponse, handleError)
            .then(user => {
                if (user && user.accessToken) {
                    authHelper.setToken(user.accessToken);
                }
                return user;
            });
    }
}

export default AccountApi;