using System.ComponentModel.DataAnnotations;

namespace OnTask.Data.Entities
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : BaseEntity
    {
        #region Table Properties
        /// <summary>
        /// Gets or sets the identifier for the <see cref="User"/> class.
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the email for the <see cref="User"/> class.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the first name for the <see cref="User"/> class.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name for the <see cref="User"/> class.
        /// </summary>
        public string LastName { get; set; } 
        #endregion
    }
}
