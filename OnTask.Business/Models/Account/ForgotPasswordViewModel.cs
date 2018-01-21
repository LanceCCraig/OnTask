using OnTask.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents data for a <see cref="User"/> that has forgotten their password.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ForgotPasswordViewModel"/> class.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
