using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides API methods related to recommendation data.
    /// </summary>
    /// <response code="401">The caller is not authenticated.</response>
    [Route("api/[controller]")]
    [ProducesResponseType(401)]
    public class RecommendationController : BaseAuthenticatedController
    {
        #region Fields
        private readonly IRecommendationService service;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The class that provides <see cref="HttpContext"/> data.</param>
        /// <param name="service">The service for interacting with recommendation data.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public RecommendationController(
            IHttpContextAccessor httpContextAccessor,
            IRecommendationService service,
            UserManager<User> userManager)
            : base(
                httpContextAccessor,
                userManager)
        {
            this.service = service;
            this.service.AddApplicationUser(ApplicationUser);
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets all <see cref="RecommendationModel"/> classes.
        /// </summary>
        /// <param name="end">The ending time period to retrieve <see cref="RecommendationModel"/> classes.</param>
        /// <returns>An <see cref="IActionResult"/> response containing the <see cref="RecommendationModel"/> classes.</returns>
        /// <response code="200">The request has succeeded and the models are returned.</response>
        /// <response code="401">The caller is not authenticated</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAll(DateTime end) => Ok(service.GetRecommendations(end));
        #endregion
    }
}
