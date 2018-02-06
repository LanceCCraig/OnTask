using System;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents a query to retrieve all <see cref="EventModel"/> classes.
    /// </summary>
    public class EventGetAllModel
    {
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventTypeModel"/> class.
        /// </summary>
        public int? EventTypeId { get; set; }
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public int? EventGroupId { get; set; }
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int? EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the optional minimum <see cref="EventModel.StartDate"/> value.
        /// </summary>
        public DateTime? DateRangeStart { get; set; }
        /// <summary>
        /// Gets or sets the optional maximum <see cref="EventModel.StartDate"/> value.
        /// </summary>
        public DateTime? DateRangeEnd { get; set; }
    }
}
