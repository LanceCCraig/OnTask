namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents a query to retrieve all <see cref="EventTypeModel"/> classes.
    /// </summary>
    public class EventTypeGetAllModel
    {
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventGroupModel"/> class.
        /// </summary>
        public int? EventGroupId { get; set; }
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int? EventParentId { get; set; }
    }
}
