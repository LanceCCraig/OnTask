using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents data for a <see cref="User"/> to reset their password.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ResetPasswordModel"/> class.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password for the <see cref="ResetPasswordModel"/> class.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the confirmation password for the <see cref="ResetPasswordModel"/> class.
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Gets or sets the reset token for the <see cref="ResetPasswordModel"/> class.
        /// </summary>
        public string Token { get; set; }
    }
}
