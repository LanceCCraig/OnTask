namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents the multiple <see cref="EventGroupModel"/> classes to be changed with a delete operation.
    /// </summary>
    public class EventGroupDeleteMultipleModel
    {
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int EventParentId { get; set; }
    }
}
