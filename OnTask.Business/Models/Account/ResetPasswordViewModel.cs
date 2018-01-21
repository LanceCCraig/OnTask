using OnTask.Common;
using OnTask.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents data for a <see cref="User"/> to reset their password.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="ResetPasswordViewModel"/> class.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password for the <see cref="ResetPasswordViewModel"/> class.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = Constants.MinimumPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the confirmation password for the <see cref="ResetPasswordViewModel"/> class.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Gets or sets the reset token for the <see cref="ResetPasswordViewModel"/> class.
        /// </summary>
        public string Token { get; set; }
    }
}
