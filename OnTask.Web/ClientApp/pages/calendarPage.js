/**
 * External dependencies
 */
import * as React from 'react';
import BigCalendar from 'react-big-calendar';
import moment from 'moment';

/**
 * Internal dependencies
 */
import TaskDialog from './taskDialog';

BigCalendar.setLocalizer(BigCalendar.momentLocalizer(moment))

class CalendarPage extends React.Component {
    render() {
        return (
        <div>
            <h1>Calendar</h1>
            <br/>
            <TaskDialog />
            <div style={{height: '85vh', overflow: 'auto', margin: '10px'}}>
            <BigCalendar
                events={
                    [
                        {
                          id: 0,
                          title: 'All Day Event very long title',
                          allDay: true,
                          start: new Date(2018, 0, 1),
                          end: new Date(2018, 0, 18),
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
                          {
                              id: 3,
                              title: 'EXPO',
                              start: new Date(2018, 3, 3),
                              end: new Date(2018, 3, 4)
                          }
                    ]
                }
                views={{
                    month: true,
                    week: true,
                  }}
                step={60}
                showMultiDayTimes
                popup
                toolbar
                startAccessor="start"
                endAccessor="end"
                defaultDate={new Date(2018, 2, 28)}
            />
            </div>
        </div>
        )
    }
}

export default CalendarPage;


