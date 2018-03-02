/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';

export default class authHelper {
    static getToken() {
        return localStorage.getItem(Constants.TOKEN_STORAGE_KEY);
    }

    static setToken(token) {
        localStorage.setItem(Constants.TOKEN_STORAGE_KEY, token);
    }

    static removeToken() {
        localStorage.removeItem(Constants.TOKEN_STORAGE_KEY);
    }
}