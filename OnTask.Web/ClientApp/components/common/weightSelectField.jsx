/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

const WeightSelectField = ({ disabled, errorText, onChange, value }) => {
    return (
        <SelectField
            style={{textAlign: "left"}}
            floatingLabelText="Importance"
            disabled={disabled}
            value={value}
            onChange={onChange}
            errorText={errorText}>
            <MenuItem value={null} primaryText="" />
            <MenuItem value={5} primaryText="Very Low" />
            <MenuItem value={4} primaryText="Low" />
            <MenuItem value={3} primaryText="Medium" />
            <MenuItem value={2} primaryText="High" />
            <MenuItem value={1} primaryText="Very High" />
        </SelectField>
    );
};

WeightSelectField.propTypes = {
    disabled: PropTypes.bool.isRequired,
    errorText: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ])
};

export default WeightSelectField;
