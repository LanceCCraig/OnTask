using OnTask.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents login data for an external <see cref="User"/> account.
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ExternalLoginViewModel"/> class.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
