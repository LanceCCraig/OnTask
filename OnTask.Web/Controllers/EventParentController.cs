﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Common;
using OnTask.Data.Entities;

namespace OnTask.Web.Controllers
{
    /// <summary>
    /// Provides API methods related to event parent data.
    /// </summary>
    [Route("api/[controller]")]
    [ProducesResponseType(401)]
    public class EventParentController : BaseAuthenticatedController
    {
        #region Fields
        private readonly IEventParentService service;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParentController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The class that provides <see cref="HttpContext"/> data.</param>
        /// <param name="service">The service for interacting with event parent data.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public EventParentController(
            IHttpContextAccessor httpContextAccessor,
            IEventParentService service,
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
        /// Creates an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventParentModel"/> class to create.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="201">The request has succeeded and the new model is returned.</response>
        /// <response code="400">The provided model is invalid.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody][CustomizeValidator(RuleSet = Constants.RuleSetNameForInsert)]EventParentModel model)
        {
            if (ModelState.IsValid)
            {
                service.Insert(model);
                return CreatedAtRoute("GetEventParent", new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to delete.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="201">The request has succeeded and nothing is returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all <see cref="EventParentModel"/> classes.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> response containing the <see cref="EventParentModel"/> classes.</returns>
        /// <response code="200">The request has succeeded and the models are returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAll() => Ok(service.GetAll());

        /// <summary>
        /// Gets an <see cref="EventParentModel"/> class by its identifier.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to get.</param>
        /// <returns>An <see cref="IActionResult"/> response containing the <see cref="EventParentModel"/> class.</returns>
        /// <response code="200">The request has succeeded and the model is returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        /// <response code="404">The model was not found.</response>
        [HttpGet("{id}", Name = "GetEventParent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var model = service.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// Updates an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to update.</param>
        /// <param name="model">The <see cref="EventParentModel"/> class to update.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="204">The request has succeeded and nothing is returned..</response>
        /// <response code="400">The provided model is invalid or the identifiers do not match.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int id, [FromBody][CustomizeValidator(RuleSet = Constants.RuleSetNameForUpdate)]EventParentModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == model.Id)
                {
                    service.Update(model);
                    return NoContent();
                }
                return BadRequest();
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
