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
    /// Provides API methods related to event data.
    /// </summary>
    /// <response code="401">The caller is not authenticated.</response>
    [Route("api/[controller]")]
    [ProducesResponseType(401)]
    public class EventController : BaseAuthenticatedController
    {
        #region Fields
        private readonly IEventService service;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The class that provides <see cref="HttpContext"/> data.</param>
        /// <param name="service">The service for interacting with event data.</param>
        /// <param name="userManager">The class that provides functionality with application <see cref="User"/> classes.</param>
        public EventController(
            IHttpContextAccessor httpContextAccessor,
            IEventService service,
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
        /// Creates an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventModel"/> class to create.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="201">The request has succeeded and the new model is returned.</response>
        /// <response code="400">The provided model is invalid.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody][CustomizeValidator(RuleSet = Constants.RuleSetNameForInsert)]EventModel model)
        {
            if (ModelState.IsValid)
            {
                service.Insert(model);
                return CreatedAtRoute("GetEvent", new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Creates multiple recurring <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The <see cref="RecurringEventModel"/> class to create the <see cref="EventModel"/> classes from.</param>
        /// <returns>An <see cref="IActionResult"/> response containing the created <see cref="EventModel"/> classes.</returns>
        /// <response code="200">The request has succeeded and the new models are returned.</response>
        /// <response code="400">The provided model is invalid.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [Route("CreateRecurring")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecurring([FromBody]RecurringEventModel model)
        {
            if (ModelState.IsValid)
            {
                var createdModels = service.InsertRecurring(model);
                return Ok(createdModels);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes multiple <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model that provides data on which <see cref="EventModel"/> classes to delete.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="201">The request has succeeded and nothing is returned.</response>
        /// <response code="400">The provided model is invalid.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult DeleteMultiple([FromBody]EventDeleteMultipleModel model)
        {
            if (ModelState.IsValid)
            {
                service.DeleteMultiple(model);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to delete.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="204">The request has succeeded and nothing is returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Gets all <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model that provides data on which <see cref="EventModel"/> classes to get.</param>
        /// <returns>An <see cref="IActionResult"/> response containing the <see cref="EventModel"/> classes.</returns>
        /// <response code="200">The request has succeeded and the models are returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAll(EventGetAllModel model) => Ok(service.GetAll(model));

        /// <summary>
        /// Gets an <see cref="EventModel"/> class by its identifier.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to get.</param>
        /// <returns>An <see cref="IActionResult"/> response containing the <see cref="EventModel"/> class.</returns>
        /// <response code="200">The request has succeeded and the model is returned.</response>
        /// <response code="401">The caller is not authenticated.</response>
        /// <response code="404">The model was not found.</response>
        [HttpGet("{id}", Name = "GetEvent")]
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
        /// Updates an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to update.</param>
        /// <param name="model">The <see cref="EventModel"/> class to update.</param>
        /// <returns>An <see cref="IActionResult"/> response.</returns>
        /// <response code="204">The request has succeeded and nothing is returned.</response>
        /// <response code="400">The provided model is invalid or the identifiers do not match.</response>
        /// <response code="401">The caller is not authenticated.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int id, [FromBody][CustomizeValidator(RuleSet = Constants.RuleSetNameForUpdate)]EventModel model)
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
