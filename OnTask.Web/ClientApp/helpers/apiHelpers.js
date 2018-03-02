export function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            var contentType = response.headers.get('content-type');
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
