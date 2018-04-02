using Microsoft.AspNetCore.Identity;
using OnTask.Business.Models.Account;
using OnTask.Business.Models.Account.Jwt;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Threading.Tasks;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides interactions with account data.
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Fields
        private readonly IJwtHandler jwtHandler;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="jwtHandler">The handler for JSON Web Tokens.</param>
        /// <param name="passwordHasher">The hashing mechanism for <see cref="User"/> passwords.</param>
        /// <param name="signInManager">The manager for authentication.</param>
        /// <param name="userManager">The manager for application <see cref="User"/> accounts.</param>
        public AccountService(
            IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.jwtHandler = jwtHandler;
            this.passwordHasher = passwordHasher;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Sends a confirmation email to a <see cref="User"/> that has forgotten their password.
        /// </summary>
        /// <param name="model">The data to send a password reset email.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null &&
                await userManager.IsEmailConfirmedAsync(user))
            {
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                // TODO: Send email confirmation.
            }
        }

        /// <summary>
        /// Generates a <see cref="JsonWebToken"/>.
        /// </summary>
        /// <param name="email">The <see cref="User"/> email that will provide the subject for the <see cref="JsonWebToken"/>.</param>
        /// <returns>The generated <see cref="JsonWebToken"/>.</returns>
        public async Task<JsonWebToken> GenerateJwt(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var jwt = jwtHandler.Create(user);
            var refreshToken = passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                        .Replace("+", string.Empty)
                        .Replace("=", string.Empty)
                        .Replace("/", string.Empty);
            jwt.RefreshToken = refreshToken;
            // TODO: Add refresh token to database.
            return jwt;
        }

        /// <summary>
        /// Signs the internal <see cref="User"/> into the application.
        /// </summary>
        /// <param name="model">The data to authenticate the <see cref="User"/>.</param>
        /// <returns>The result from the attempted sign in.</returns>
        public async Task<SignInResult> Login(LoginModel model) => await signInManager.PasswordSignInAsync(
            model.Email,
            model.Password, model.RememberMe,
            false);

        /// <summary>
        /// Signs the <see cref="User"/> out of the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task Logout() => await signInManager.SignOutAsync();

        /// <summary>
        /// Registers an internal <see cref="User"/>.
        /// </summary>
        /// <param name="model">The data to register the <see cref="User"/>.</param>
        /// <returns>The result from the attempted registration.</returns>
        public async Task<IdentityResult> Register(RegisterModel model) => await userManager.CreateAsync(
            new User
            {
                UserName = model.Email,
                Email = model.Email
            },
            model.Password);

        /// <summary>
        /// Resets the password for a <see cref="User"/>.
        /// </summary>
        /// <param name="model">The data to reset the password for the <see cref="User"/>.</param>
        /// <returns>The result from the attempted password reset.</returns>
        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync(
                    user,
                    model.Token,
                    model.Password);
            }
            return null;
        }

        /// <summary>
        /// Sends an email to a <see cref="User"/> to confirm their email address.
        /// </summary>
        /// <param name="email">The email of the <see cref="User"/> to confirm.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task SendEmailConfirmation(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            // TODO: Send email confirmation.
        }
        #endregion
    }
}
