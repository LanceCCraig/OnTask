using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents a query to retrieve all <see cref="EventGroupModel"/> classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventGroupGetAllModel
    {
        /// <summary>
        /// Gets or sets the optional identifier for the associated <see cref="EventParentModel"/> class.
        /// </summary>
        public int? EventParentId { get; set; }
    }
}
