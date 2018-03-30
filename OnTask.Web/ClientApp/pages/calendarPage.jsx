/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import BigCalendar from 'react-big-calendar';
import moment from 'moment';

/**
 * Internal dependencies
 */
import CalendarToolbar from 'ClientApp/components/calendar/calendarToolbar';

BigCalendar.setLocalizer(BigCalendar.momentLocalizer(moment))

class CalendarPage extends React.Component {
    constructor(props, context) {
        super(props, context);
    }

    render() {
        const allViews = Object.keys(BigCalendar.Views).map(key => BigCalendar.Views[key]);
        const { events } = this.props;
        return (
            <div style={{ height: 800 }}>
                <BigCalendar
                    events={events}
                    components={{
                        toolbar: CalendarToolbar
                    }}
                    views={allViews}
                    step={30}
                    showMultiDayTimes
                    popup
                    toolbar={true}
                    defaultDate={new Date()}
                    titleAccessor="name"
                    startAccessor="startDateTime"
                    endAccessor="endDateTime"
                    allDayAccessor="isAllDay"
                />
            </div>
        );
    }
}

CalendarPage.propTypes = {
    events: PropTypes.array,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        events: state.events
    };
}

function mapDispatchToProps(dispatch) {
    return {
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(CalendarPage);
