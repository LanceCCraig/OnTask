using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using OnTask.Business.Models.Event;
using OnTask.Business.Services.Interfaces;
using OnTask.Common.Injections;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTask.Business.Services
{
    /// <summary>
    /// Provides the service for interacting with <see cref="EventModel"/> classes.
    /// </summary>
    public class EventService : BaseService, IEventService
    {
        #region Fields
        private IOnTaskDbContext context;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that the service will interact with.</param>
        public EventService(IOnTaskDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to delete.</param>
        public void Delete(int id)
        {
            try
            {
                var entity = context.GetEventByIdTracked(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    context.DeleteEvent(entity); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes multiple <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventModel"/> classes to delete.</param>
        public void DeleteMultiple(EventDeleteMultipleModel model)
        {
            try
            {
                context.DeleteEvents(context.GetEventsTracked(ApplicationUser.Id, model.EventTypeId, model.EventGroupId, model.EventParentId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventModel"/> classes.</returns>
        public IEnumerable<EventModel> GetAll(EventGetAllModel model)
        {
            try
            {
                return context
                    .GetEvents(
                        ApplicationUser.Id,
                        model.EventTypeId,
                        model.EventGroupId,
                        model.EventParentId,
                        model.DateRangeStart,
                        model.DateRangeEnd)
                    .Select(x => GetModelFromEntity(x))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to get.</param>
        /// <returns>The <see cref="EventModel"/> class.</returns>
        public EventModel GetById(int id)
        {
            try
            {
                return GetModelFromEntity(context.GetEventById(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventModel"/> class to insert.</param>
        public void Insert(EventModel model)
        {
            try
            {
                var entity = (Event)new Event
                {
                    UserId = ApplicationUser.Id,
                    CreatedOn = DateTime.Now
                }.InjectFrom<SmartInjection>(model);
                context.InsertEvent(entity);
                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventModel"/> class to update.</param>
        public void Update(EventModel model)
        {
            try
            {
                var entity = context.GetEventByIdTracked(model.Id.Value);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    entity.InjectFrom<SmartInjection>(model);
                    entity.UpdatedOn = DateTime.Now;
                    context.SaveChanges(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Private Helpers
        private EventModel GetModelFromEntity(Event entity) => (EventModel)new EventModel
        {
            EventGroupName = entity.EventGroup.Name,
            EventParentName = entity.EventParent.Name,
            EventTypeName = entity.EventType.Name
        }.InjectFrom<SmartInjection>(entity);
        #endregion
    }
}
