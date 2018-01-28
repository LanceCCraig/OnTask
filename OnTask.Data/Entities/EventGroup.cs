using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTask.Data.Entities
{
    /// <summary>
    /// Represents the group of an <see cref="Event"/> class.
    /// </summary>
    public class EventGroup : BaseEntity
    {
        #region Table Properties
        /// <summary>
        /// Gets or sets the identifier for the <see cref="EventGroup"/> class.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.EventParent"/> class.
        /// </summary>
        [ForeignKey(nameof(EventParent))]
        public int EventParentId { get; set; }
        /// <summary>
        /// Gets or sets the identifier for the associated <see cref="Entities.User"/> class.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the name for the <see cref="EventGroup"/> class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the display name for the <see cref="EventGroup"/> class.
        /// </summary>
        public string DisplayName { get; set; }
        #endregion

        #region External Properties
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.EventParent"/> class.
        /// </summary>
        public EventParent EventParent { get; set; }
        /// <summary>
        /// Gets or sets the associated <see cref="Entities.User"/> class.
        /// </summary>
        public User User { get; set; }
        #endregion
    }
}
