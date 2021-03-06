﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnTask.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides a base <see cref="Controller"/> for the application in which the user must be authenticated.
    /// </summary>
    [Authorize]
    public class BaseAuthenticatedController : Controller
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAuthenticatedController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The class that provides <see cref="HttpContext"/> data.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public BaseAuthenticatedController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            var email = httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            ApplicationUser = userManager.FindByEmailAsync(email).Result;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current user for the <see cref="Controller"/> class.
        /// </summary>
        protected User ApplicationUser { get; private set; }
        #endregion
    }
}
