/**
 * External dependencies
 */
import { push } from 'react-router-redux';
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import * as types from 'ClientApp/actions/actionTypes';
import accountApi from 'ClientApp/api/accountApi';

export function register(username, password, confirmPassword) {
    return function(dispatch) {
        dispatch(request({ username }));

        return accountApi.register(username, password, confirmPassword)
            .then(
                user => {
                    dispatch(success(user));
                    toastr.success('User created successfully.');
                    dispatch(push('/login'));
                },
                errors => {
                    dispatch(failure(errors));
                    toastr.error(Object.values(errors)[0]);
            });
    };

    function request(user) { return { type: types.REGISTER_REQUEST, user }; }
    function success(user) { return { type: types.REGISTER_SUCCESS, user }; }
    function failure(errors) { return { type: types.REGISTER_FAILURE, errors }; }
}

export function login(username, password) {
    return function(dispatch) {
        dispatch(request({ username }));
        
        return accountApi.login(username, password)
            .then(
                user => {
                    dispatch(success(user));
                    toastr.success('Login successful.');
                    dispatch(push('/'));
                },
                errors => {
                    dispatch(failure(errors));
                    toastr.error('Login failed.');
            });
    };

    function request(user) { return { type: types.LOGIN_REQUEST, user }; }
    function success(user) { return { type: types.LOGIN_SUCCESS, user }; }
    function failure(errors) { return { type: types.LOGIN_FAILURE, errors }; }
}

export function logout() {
    return function (dispatch) {
        return accountApi.logout().then(
            user => {
                toastr.success('Logout successful.');
            },
            error => {
                toastr.error('Logout failed.');
            });
    };
}

export function forgotPassword(username) {
    return function (dispatch) {
        dispatch(request({ username }));

        return accountApi.forgotPassword().then(
            user => {
                dispatch(success(user));
                toastr.success('Acknowledged.');
                dispatch.push('/');
            },
            error => {
                toastr.error('Acknowledge Failed.');
            });
    };

    function request(user) { return { type: types.PASSWORD_REQUEST, user }; }
    function success(user) { return { type: types.PASSWORD_SUCCESS, user }; }
    function failure(errors) { return { type: types.PASSWORD_FAILURE, errors }; }
}
