using System;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an <see cref="Data.Entities.Event"/> class with all necessary properties.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventFullModel
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
        /// Gets or sets the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public EventGroupModel EventGroup { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public EventParentModel EventParent { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public EventTypeModel EventType { get; set; }
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
        /// <summary>
        /// Gets or sets the weight for the <see cref="EventModel"/> class.
        /// </summary>
        public int? Weight { get; set; }
    }
}
