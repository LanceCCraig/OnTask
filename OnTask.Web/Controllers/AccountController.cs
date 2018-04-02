using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnTask.Business.Models.Account;
using OnTask.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides API methods related to account data.
    /// </summary>
    // TODO: Configure ValidateAntiForgeryToken to work with Swagger.
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        #region Fields
        private readonly IAccountService accountService;
        private readonly ILogger<AccountController> logger;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The service for interacting with account data.</param>
        /// <param name="logger">The class that provides logging for the controller.</param>
        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            this.logger = logger;
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
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The provided model is invalid.</response>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                await accountService.ForgotPassword(model);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Signs the internal user into the application.
        /// </summary>
        /// <param name="model">The data to authenticate the user.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The provided model is invalid.</response>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.Login(model);
                if (result.Succeeded)
                {
                    var jwt = await accountService.GenerateJwt(model.Email);
                    return Ok(jwt);
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
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="200">The request has succeeded.</response>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            await accountService.Logout();
            return Ok();
        }

        /// <summary>
        /// Registers an internal user.
        /// </summary>
        /// <param name="model">The data to register the user.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The provided model is invalid.</response>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.Register(model);
                if (result.Succeeded)
                {
                    await accountService.SendEmailConfirmation(model.Email);
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
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The provided model is invalid.</response>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.ResetPassword(model);
                if (!result.Succeeded)
                {
                    AddIdentityResultErrors(result);
                    return BadRequest(ModelState);
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
