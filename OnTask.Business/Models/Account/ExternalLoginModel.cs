using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents login data for an external <see cref="User"/> account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExternalLoginModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ExternalLoginModel"/> class.
        /// </summary>
        public string Email { get; set; }
    }
}
