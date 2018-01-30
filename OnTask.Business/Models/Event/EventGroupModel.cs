using OnTask.Data.Entities;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an <see cref="Data.Entities.EventGroup"/> class.
    /// </summary>
    public class EventGroupModel
    {
        /// <summary>
        /// Gets or sets the identifier for the <see cref="EventGroupModel"/> class if it exists.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="EventParentModel"/> class if it exists.
        /// </summary>
        public int? EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the name for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public string EventParentName { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="User"/> class.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the name for the <see cref="EventGroupModel"/> class.
        /// </summary>
        public string Name { get; set; }
    }
}
