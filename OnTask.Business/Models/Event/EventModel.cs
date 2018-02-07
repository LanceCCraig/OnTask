using System;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an <see cref="Data.Entities.Event"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventModel
    {
        /// <summary>
        /// Gets or sets the identifier for the <see cref="EventModel"/> class if it exists.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public int EventGroupId { get; set; }
        /// <summary>
        /// Gets or sets the name for the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public string EventGroupName { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the name for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public string EventParentName { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// Gets or sets the name for the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public string EventTypeName { get; set; }
        /// <summary>
        /// Gets or sets the name for the <see cref="EventModel"/> class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description for the <see cref="EventModel"/> class.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the start date for the <see cref="EventModel"/> class.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date for the <see cref="EventModel"/> class.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
