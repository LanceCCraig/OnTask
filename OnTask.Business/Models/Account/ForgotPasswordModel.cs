using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents data for a <see cref="User"/> that has forgotten their password.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ForgotPasswordModel"/> class.
        /// </summary>
        public string Email { get; set; }
    }
}
