using System;
using System.Collections.Generic;
using static OnTask.Common.Enumerations;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an event that will be created for multiple dates.
    /// </summary>
    public class RecurringEventModel
    {
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public int EventGroupId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// Gets or sets the name for the recurring <see cref="EventModel"/> classes.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description for the recurring <see cref="EventModel"/> classes.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the weight for the recurring <see cref="EventModel"/> classes.
        /// </summary>
        public int? Weight { get; set; }
        /// <summary>
        /// Gets or sets the start time for the recurring <see cref="EventModel"/> classes.
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        /// <summary>
        /// Gets or sets the end time for the recurring <see cref="EventModel"/> classes.
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        /// <summary>
        /// Gets or sets the start date for the <see cref="RecurringEventModel"/> class.
        /// </summary>
        public DateTime DateRangeStart { get; set; }
        /// <summary>
        /// Gets or sets the end date for the <see cref="RecurringEventModel"/> class.
        /// </summary>
        public DateTime DateRangeEnd { get; set; }
        /// <summary>
        /// Gets or sets the value that determines whether the created <see cref="EventModel"/> classes last all day.
        /// </summary>
        public bool IsAllDay { get; set; }
        /// <summary>
        /// Gets or sets the days of the week for the <see cref="RecurringEventModel"/> class.
        /// </summary>
        public IEnumerable<string> DaysOfWeek { get; set; }
    }
}
