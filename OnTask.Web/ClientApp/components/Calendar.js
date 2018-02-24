import * as React from 'react';
import BigCalendar from 'react-big-calendar';
import moment from 'moment';
import { RouteComponentProps } from 'react-router-dom';

BigCalendar.momentLocalizer(moment);

class Calendar extends React.Component {
    render() {
        return (
        <div>
            <h1>Calendar</h1>
            <p>This page is under construction! We'll be putting a calendar here soon!</p>
            <BigCalendar
                events={
                    [
                        {
                          id: 0,
                          title: 'All Day Event very long title',
                          allDay: true,
                          start: new Date(2018, 1, 18),
                          end: new Date(2018, 1, 1),
                        },
                        {
                            id: 1,
                            title: 'Long Event',
                            start: new Date(2018, 1, 7),
                            end: new Date(2018, 1, 10),
                          },
                        
                          {
                            id: 2,
                            title: 'DTS STARTS',
                            start: new Date(2018, 1, 13, 0, 0, 0),
                            end: new Date(2018, 1, 20, 0, 0, 0),
                          },
                    ]
                }
                views={['month', 'week', 'day']}
                step={60}
                showMultiDayTimes
                startAccessor="start"
                endAccessor="end"
                defaultDate={new Date(2018, 1, 21)}
            />
        </div>
        )
    }
}

export default Calendar;
