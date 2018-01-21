using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnTask.Business.Models.Account;
using OnTask.Data.Entities;
using System.Threading.Tasks;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides API methods related to account data.
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    // TODO: Configure ValidateAntiForgeryToken to work with Swagger.
    public class AccountController : Controller
    {
        #region Fields
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">The class that provides logging for the controller.</param>
        /// <param name="signInManager">The class that provides authentication functionality.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public AccountController(
            ILogger<AccountController> logger,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        #endregion

        #region Public Interface
        /* TODO: Setup external authentication.
        public IActionResult ExternalLogin(string provider)
        {
            //var redirectUrl = Url.Action()
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            
        }

        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model)
        {

        }
        */

        /// <summary>
        /// Sends a confirmation email to a user that has forgotten their password.
        /// </summary>
        /// <param name="model">The data to send a password reset email.</param>
        /// <returns>An <see cref="IActionResult"/> representing the status of the request.</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null &&
                    await userManager.IsEmailConfirmedAsync(user))
                {
                    var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                    // TODO: Send email confirmation.
                }
            }
            return Ok();
        }

        /// <summary>
        /// Signs the internal user into the application.
        /// </summary>
        /// <param name="model">The data to authenticate the user.</param>
        /// <returns>An <see cref="IActionResult"/> representing the status of the request.</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    false);
                if (result.Succeeded)
                {
                    return Ok();
                }
                // TODO: Add 2FA & Lockout cases.
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Signs the user out of the application.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the status of the request.</returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Registers an internal user.
        /// </summary>
        /// <param name="model">The data to register the user.</param>
        /// <returns>An <see cref="IActionResult"/> representing the status of the request.</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(
                    user,
                    model.Password);
                if (result.Succeeded)
                {
                    var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    // TODO: Send email confirmation.

                    await signInManager.SignInAsync(
                        user,
                        false);
                    return Ok();
                }
                AddIdentityResultErrors(result);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Resets the a user's password.
        /// </summary>
        /// <param name="model">The data to reset the user's password.</param>
        /// <returns>An <see cref="IActionResult"/> representing the status of the request.</returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(
                        user,
                        model.Token,
                        model.Password);
                    if (!result.Succeeded)
                    {
                        AddIdentityResultErrors(result);
                        return BadRequest(ModelState);
                    }
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Private Helpers
        private void AddIdentityResultErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
