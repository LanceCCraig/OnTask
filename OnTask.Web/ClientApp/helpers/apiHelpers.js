/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';

export function getAuthorizedHeaders() {
    let headers = {
        'Authorization': `Bearer ${authHelper.getToken()}`,
        'Content-Type': 'application/json'
    };
    return headers;
}

export function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            var contentType = response.headers.get('Content-Type');
            if (contentType && contentType.includes('application/json')) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            response.json().then(json => reject(json));
        }
    });
}

export function handleError(error) {
    return Promise.reject(error && error.message);
}
