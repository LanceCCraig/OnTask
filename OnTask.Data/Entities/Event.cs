﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Data.Entities
{
    /// <summary>
    /// Represents an event on the calendar.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Event : BaseEntity
    {
        #region Table Properties
        /// <summary>
        /// Gets or sets the identifier for the <see cref="Event"/> class.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.EventGroup"/> class.
        /// </summary>
        [ForeignKey(nameof(EventGroup))]
        public int EventGroupId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.EventParent"/> class.
        /// </summary>
        [ForeignKey(nameof(EventParent))]
        public int EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.EventType"/> class.
        /// </summary>
        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.User"/> class.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the name for the <see cref="Event"/> class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description for the <see cref="Event"/> class.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the start date for the <see cref="Event"/> class.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the start time for the <see cref="Event"/> class.
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        /// <summary>
        /// Gets or sets the end date for the <see cref="Event"/> class.
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Gets or sets the end time for the <see cref="Event"/> class.
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        /// <summary>
        /// Gets or sets the value that determines whether the <see cref="Event"/> class lasts the entire day.
        /// </summary>
        public bool IsAllDay { get; set; }
        /// <summary>
        /// Gets or sets the weight for the <see cref="Event"/> class.
        /// </summary>
        public int? Weight { get; set; }
        #endregion

        #region External Properties
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.EventGroup"/> class.
        /// </summary>
        public EventGroup EventGroup { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.EventParent"/> class.
        /// </summary>
        public EventParent EventParent { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.EventType"/> class.
        /// </summary>
        public EventType EventType { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.User"/> class.
        /// </summary>
        public User User { get; set; }
        #endregion
    }
}
