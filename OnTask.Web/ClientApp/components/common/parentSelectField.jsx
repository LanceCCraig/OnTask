/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

const ParentSelectField = ({ eventParents, disabled, errorText, onChange, value }) => {
    return (
        <SelectField
            style={{textAlign: "left"}}
            floatingLabelText="Parent"
            disabled={disabled}
            value={value}
            onChange={onChange}
            errorText={errorText}>
            <MenuItem value={null} primaryText="" />
            {eventParents.map(eventParent =>
                <MenuItem value={eventParent.id} primaryText={eventParent.name} />
            )}<br />
        </SelectField>
    );
};

ParentSelectField.propTypes = {
    eventParents: PropTypes.array.isRequired,
    disabled: PropTypes.bool.isRequired,
    errorText: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ])
};

export default ParentSelectField;
