using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an <see cref="Data.Entities.EventType"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventTypeModel
    {
        /// <summary>
        /// Gets or sets the identifier for the <see cref="EventTypeModel"/> class if it exists.
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
        /// Gets or sets the name for the <see cref="EventTypeModel"/> class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description for the <see cref="EventTypeModel"/> class.
        /// </summary>
        public string Description { get; set; }
    }
}
