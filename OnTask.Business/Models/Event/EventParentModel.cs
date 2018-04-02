using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents an <see cref="Data.Entities.EventParent"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventParentModel
    {
        /// <summary>
        /// Gets or sets the identifier for the <see cref="EventParentModel"/> class if it exists.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Gets or sets the name for the <see cref="EventParentModel"/> class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description for the <see cref="EventParentModel"/> class.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the weight for the <see cref="EventModel"/> class.
        /// </summary>
        public int? Weight { get; set; }
    }
}
