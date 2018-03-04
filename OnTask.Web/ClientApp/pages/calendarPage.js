/**
 * External dependencies
 */
import * as React from 'react';
import BigCalendar from 'react-big-calendar';
import moment from 'moment';
import TaskDialog from './taskDialog';

BigCalendar.setLocalizer(BigCalendar.momentLocalizer(moment))

class CalendarPage extends React.Component {
    render() {
        return (
        <div>
            <h1>Calendar</h1>
            <br/>
            {/* <p>This page is under construction! We'll be putting a calendar here soon!</p> */}
            {/* <button onClick="" style={{backgroundColor: 'lightblue', margin: '10px'}}>Add Task</button> */}
            
            <TaskDialog />
            
            <div style={{height: '85vh', overflow: 'auto', margin: '10px'}}>
            <BigCalendar
                // interface View {
                //     static navigate(date: Date, action: 'PREV' | 'NEXT' | 'DATE'): Date
                //   }
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
                defaultDate={new Date(2018, 1, 21)}
            />
            </div>
        </div>
        )
    }
}

export default CalendarPage;


