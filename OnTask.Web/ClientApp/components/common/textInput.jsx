/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';

const TextInput = ({name, label, onChange, placeholder, disabled, value, error}) => {
    let wrapperClass = 'form-group';
    if (error !== undefined) {
        wrapperClass += ' has-error';
    }

    let errors;
    if (Array.isArray(error)) {
        errors = error.map((err) =>
            <li>{err}</li>
        );
    }

    return (
        <div className={wrapperClass}>
            <label htmlFor={name}>{label}</label>
            <div className="field">
                <input
                    type="text"
                    name={name}
                    className="form-control"
                    placeholder={placeholder}
                    disabled={disabled}
                    value={value}
                    onChange={onChange}/>
                {error && !errors && <div className="alert alert-danger">{error}</div>}
                {error && errors &&
                    <div className="alert alert-danger">
                        <ul>{errors}</ul>
                    </div>}
            </div>
        </div>
    );
};

TextInput.propTypes = {
    name: PropTypes.string.isRequired,
    label: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
    placeholder: PropTypes.string,
    disabled: PropTypes.bool,
    value: PropTypes.string,
    error: PropTypes.oneOfType([
        PropTypes.string,
        PropTypes.array
    ])
};

export default TextInput;
