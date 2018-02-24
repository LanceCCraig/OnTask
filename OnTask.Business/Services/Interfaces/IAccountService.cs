using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnTask.Business.Models.Account;
using OnTask.Business.Models.Account.Jwt;
using OnTask.Data.Entities;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes interactions with account data.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Sends a confirmation email to a <see cref="User"/> that has forgotten their password.
        /// </summary>
        /// <param name="model">The data to send a password reset email.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task ForgotPassword(ForgotPasswordModel model);
        /// <summary>
        /// Generates a <see cref="JsonWebToken"/>.
        /// </summary>
        /// <param name="email">The <see cref="User"/> email that will provide the subject for the <see cref="JsonWebToken"/>.</param>
        /// <returns>The generated <see cref="JsonWebToken"/>.</returns>
        Task<JsonWebToken> GenerateJwt(string email);
        /// <summary>
        /// Signs the internal <see cref="User"/> into the application.
        /// </summary>
        /// <param name="model">The data to authenticate the <see cref="User"/>.</param>
        /// <returns>The result from the attempted sign in.</returns>
        Task<SignInResult> Login(LoginModel model);
        /// <summary>
        /// Signs the <see cref="User"/> out of the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task Logout();
        /// <summary>
        /// Registers an internal <see cref="User"/>.
        /// </summary>
        /// <param name="model">The data to register the <see cref="User"/>.</param>
        /// <returns>The result from the attempted registration.</returns>
        Task<IdentityResult> Register(RegisterModel model);
        /// <summary>
        /// Resets the password for a <see cref="User"/>.
        /// </summary>
        /// <param name="model">The data to reset the password for the <see cref="User"/>.</param>
        /// <returns>The result from the attempted password reset.</returns>
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        /// <summary>
        /// Sends an email to a <see cref="User"/> to confirm their email address.
        /// </summary>
        /// <param name="email">The email of the <see cref="User"/> to confirm.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task SendEmailConfirmation(string email);
    }
}