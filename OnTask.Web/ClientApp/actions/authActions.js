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

    function request(user) { return { type: types.LOGIN_REQUEST, user } }
    function success(user) { return { type: types.LOGIN_SUCCESS, user } }
    function failure(errors) { return { type: types.LOGIN_FAILURE, errors } }
}