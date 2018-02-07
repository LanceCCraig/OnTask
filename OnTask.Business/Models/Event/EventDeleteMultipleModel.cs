using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents the multiple <see cref="EventModel"/> classes to be changed with a delete operation.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventDeleteMultipleModel
    {
        /// <summary>
        /// Gets or sets the mode to retrieve <see cref="EventModel"/> classes by.
        /// </summary>
        public string Mode { get; set; }
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
    }
}
