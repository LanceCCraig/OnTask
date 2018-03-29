/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';

export default {
    auth: {
        errors: {},
        loggedIn: authHelper.getToken() ? true : false,
        loggingIn: false,
        token: authHelper.getToken(),
        user: {}
    },
    eventParents: [],
    eventGroups: [],
    eventTypes: []
};