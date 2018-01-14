using System;

namespace OnTask.Data.Entities
{
    /// <summary>
    /// Provides basic properties for all entities.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the date when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the date when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
