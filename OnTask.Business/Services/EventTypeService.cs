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
    /// Provides the service for interacting with <see cref="EventTypeModel"/> classes.
    /// </summary>
    public class EventTypeService : BaseService, IEventTypeService
    {
        #region Fields
        private IOnTaskDbContext context;
        private IMapperService mapper;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that the service will interact with.</param>
        /// <param name="mapper">The service which provides mappings from <see cref="EventType"/> classes to <see cref="EventTypeModel"/> classes.</param>
        public EventTypeService(IOnTaskDbContext context, IMapperService mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventTypeModel"/> class to delete.</param>
        public void Delete(int id)
        {
            try
            {
                var entity = context.GetEventTypeByIdTracked(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    context.DeleteEventType(entity); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes multiple <see cref="EventTypeModel"/> classes.
        /// </summary>
        /// <param name="model">The model which rovides data on which <see cref="EventTypeModel"/> classes to delete.</param>
        public void DeleteMultiple(EventTypeDeleteMultipleModel model)
        {
            try
            {
                context.DeleteEventTypes(context.GetEventTypesTracked(ApplicationUser.Id, model.EventGroupId, model.EventParentId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets <see cref="EventTypeModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventTypeModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventTypeModel"/> classes.</returns>
        public IEnumerable<EventTypeModel> GetAll(EventTypeGetAllModel model)
        {
            try
            {
                return context
                    .GetEventTypes(
                        ApplicationUser.Id,
                        model.EventGroupId,
                        model.EventParentId)
                    .Select(x => mapper.Map<EventTypeModel>(x))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventTypeModel"/> class to get.</param>
        /// <returns>The <see cref="EventTypeModel"/> class.</returns>
        public EventTypeModel GetById(int id)
        {
            try
            {
                var model = default(EventTypeModel);
                var entity = context.GetEventTypeById(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    model = mapper.Map<EventTypeModel>(entity);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventTypeModel"/> class to insert.</param>
        public void Insert(EventTypeModel model)
        {
            try
            {
                var entity = (EventType)new EventType
                {
                    UserId = ApplicationUser.Id,
                    CreatedOn = DateTime.Now
                }.InjectFrom<SmartInjection>(model);
                context.InsertEventType(entity);
                model.InjectFrom<SmartInjection>(GetById(entity.Id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventTypeModel"/> class to update.</param>
        public void Update(EventTypeModel model)
        {
            try
            {
                var entity = context.GetEventTypeByIdTracked(model.Id.Value);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    entity.InjectFrom<SmartInjection>(model);
                    entity.UpdatedOn = DateTime.Now;
                    context.SaveChanges();
                    model.InjectFrom<SmartInjection>(GetById(entity.Id));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
