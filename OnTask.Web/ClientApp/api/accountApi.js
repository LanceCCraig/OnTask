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
    forgotPassword(email) {
        const requestOptions = {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify({
                email: email
            })
        };

        return fetch(`${Constants.ACCOUNT_API_URL}/ForgotPassword`, requestOptions)
            .then(handleResponse, handleError);
    },

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
    },

    logout() {
        const requestOptions = {
            method: 'POST',
            headers: { 'content-type': 'application/json' }
        };

        return fetch(`${Constants.ACCOUNT_API_URL}/Logout`, requestOptions)
            .then(handleResponse, handleError)
            .then(authHelper.removeToken());
    },

    register(email, password, confirmPassword) {
        const requestOptions = {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify({
                email: email,
                password: password,
                confirmPassword: confirmPassword
            })
        };

        return fetch(`${Constants.ACCOUNT_API_URL}/Register`, requestOptions)
            .then(handleResponse, handleError);
    },

    resetPassword(email, password, confirmPassword, token) {
        const requestOptions = {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify({
                email: email,
                password: password,
                confirmPassword: confirmPassword,
                token: token
            })
        };

        return fetch(`${Constants.ACCOUNT_API_URL}/ResetPassword`, requestOptions)
            .then(handleResponse, handleError);
    }
};

export default AccountApi;