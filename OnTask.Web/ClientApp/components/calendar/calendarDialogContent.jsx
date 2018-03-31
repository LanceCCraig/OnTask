/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';

const CalendarDialogContent = ({
    name,
    description,
    eventParentName,
    eventGroupName,
    eventTypeName,
    isAllDay,
    startDateTime,
    endDateTime,
    weight
}) => {
    return (
        <div>
            <div className="modal-header">
                <h4 className="modal-title">{name}</h4>
            </div>
            <div className="modal-body">
                <dl className="dl-horizontal">
                    <dt>Description</dt>
                    <dd>{description !== null && description !== '' ? description : 'N/A'}</dd>
                    <dt>Parent</dt>
                    <dd>{eventParentName}</dd>
                    <dt>Group</dt>
                    <dd>{eventGroupName}</dd>
                    <dt>Type</dt>
                    <dd>{eventTypeName}</dd>
                    <dt>Lasts all day</dt>
                    <dd>{isAllDay ? 'Yes' : 'No'}</dd>
                    {isAllDay &&
                        <dt>Date</dt>
                    }
                    {isAllDay &&
                        <dd>{moment(startDateTime).format('MMM DD, YYYY')}</dd>
                    }
                    {!isAllDay &&
                        <dt>Start</dt>
                    }
                    {!isAllDay &&
                        <dd>{moment(startDateTime).format('MMM DD, YYYY LT')}</dd>
                    }
                    {!isAllDay &&
                        <dt>End</dt>
                    }
                    {!isAllDay &&
                        <dd>{moment(endDateTime).format('MMM DD, YYYY LT')}</dd>
                    }
                    <dt>Importance</dt>
                    <dd>{weight !== undefined &&
                        weight !== '' &&
                        weight !== null ? Constants.WEIGHTS.find(w => w.value === weight).name : 'N/A'}</dd>
                </dl>
            </div>
        </div>
    );
};

CalendarDialogContent.propTypes = {
    name: PropTypes.string,
    description: PropTypes.string,
    eventParentName: PropTypes.string,
    eventGroupName: PropTypes.string,
    eventTypeName: PropTypes.string,
    isAllDay: PropTypes.bool,
    startDateTime: PropTypes.object,
    endDateTime: PropTypes.object,
    weight: PropTypes.oneOfType([
        PropTypes.number,
        PropTypes.string
    ])
};

export default CalendarDialogContent;