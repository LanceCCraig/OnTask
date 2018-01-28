﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnTask.Data.Entities;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides API methods related to event data.
    /// </summary>
    public class EventController : BaseController
    {
        #region Fields

        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The class that provides <see cref="HttpContext"/> data.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public EventController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
            : base(
                httpContextAccessor,
                userManager)
        {
        }
        #endregion

        #region Public Interface
        
        #endregion
    }
}
