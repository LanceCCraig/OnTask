/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

const GroupSelectField = ({ eventGroups, eventParentId, disabled, errorText, onChange, value }) => {
    function filterGroups(element) {
        return eventParentId !== null &&
            eventParentId !== '' &&
            element.eventParentId === eventParentId
    }

    return (
        <SelectField
            style={{ textAlign: "left" }}
            floatingLabelText="Group"
            disabled={disabled}
            value={value}
            onChange={onChange}
            errorText={errorText}
            errorStyle={{ color: "#FF8F3A" }} >
            <MenuItem value={null} primaryText="" />
            {eventGroups.filter(filterGroups)
                .map(eventGroup =>
                <MenuItem key={eventGroup.id} value={eventGroup.id} primaryText={eventGroup.name} />
            )}<br />
        </SelectField>
    );
};

GroupSelectField.propTypes = {
    eventGroups: PropTypes.array.isRequired,
    eventParentId: PropTypes.oneOfType([
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

export default GroupSelectField;
