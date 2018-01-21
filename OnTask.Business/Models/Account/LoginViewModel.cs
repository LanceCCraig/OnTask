using OnTask.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTask.Business.Models.Account
{
    /// <summary>
    /// Represents login data for a local <see cref="User"/> account.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email for the <see cref="LoginViewModel"/> class.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password for the <see cref="LoginViewModel"/> class.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the value that determines whether the <see cref="Email"/> will be saved.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
