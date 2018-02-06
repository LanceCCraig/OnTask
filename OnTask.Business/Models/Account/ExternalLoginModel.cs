using OnTask.Data.Entities;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents login data for an external <see cref="User"/> account.
    /// </summary>
    public class ExternalLoginModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ExternalLoginModel"/> class.
        /// </summary>
        public string Email { get; set; }
    }
}
