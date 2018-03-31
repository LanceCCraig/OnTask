using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Event
{
    /// <summary>
    /// Represents a recommendation for completion of an <see cref="EventModel"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RecommendationModel
    {
        /// <summary>
        /// Gets or sets the temporary identifier for the <see cref="RecommendationModel"/> class.
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="EventModel"/> class.
        /// </summary>
        public EventModel Event { get; set; }
        /// <summary>
        /// Gets or sets the calculated priority for the associated <see cref="EventModel"/> class.
        /// </summary>
        [JsonIgnore]
        public int Priority { get; set; }
        /// <summary>
        /// Gets or sets the recommended start date for the associated <see cref="EventModel"/> class.
        /// </summary>
        public DateTime RecommendedStartDate { get; set; }
    }
}
