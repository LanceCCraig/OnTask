using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents login data for a local <see cref="User"/> account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="LoginModel"/> class.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password for the <see cref="LoginModel"/> class.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the value that determines whether the <see cref="Email"/> will be saved.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
