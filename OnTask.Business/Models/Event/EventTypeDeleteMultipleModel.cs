namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents the multiple <see cref="EventTypeModel"/> classes to be changed with a delete operation.
    /// </summary>
    public class EventTypeDeleteMultipleModel
    {
        /// <summary>
        /// Gets or sets the parent class to retrieve <see cref="EventTypeModel"/> classes by.
        /// </summary>
        public string Parent { get; set; }
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
