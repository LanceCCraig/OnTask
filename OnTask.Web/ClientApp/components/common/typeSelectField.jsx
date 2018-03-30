/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

const TypeSelectField = ({ eventTypes, eventParentId, eventGroupId, disabled, errorText, onChange, value }) => {
    function filterTypes(element) {
        return (eventParentId !== null && eventParentId !== '' && element.eventParentId === eventParentId) &&
            (eventGroupId !== null && eventGroupId !== '' && element.eventGroupId === eventGroupId);
    }

    return (
        <SelectField
            style={{ textAlign: "left" }}
            floatingLabelText="Type"
            disabled={disabled}
            value={value}
            onChange={onChange}
            errorText={errorText}
            errorStyle={{ color: "#FF8F3A" }}>
            <MenuItem value={null} primaryText="" />
            {eventTypes.filter(filterTypes)
                .map(eventType =>
                    <MenuItem key={eventType.id} value={eventType.id} primaryText={eventType.name} />
            )}<br />
        </SelectField>
    );
};

TypeSelectField.propTypes = {
    eventTypes: PropTypes.array.isRequired,
    eventParentId: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ]),
    eventGroupId: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ]),
    disabled: PropTypes.bool.isRequired,
    errorText: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ])
};

export default TypeSelectField;
