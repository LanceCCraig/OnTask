using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents registration data for a local <see cref="User"/> account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="RegisterModel"/> class.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password for the <see cref="RegisterModel"/> class.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the confirmation password for the <see cref="RegisterModel"/> class.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
