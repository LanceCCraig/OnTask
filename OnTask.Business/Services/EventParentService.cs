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
    /// Provides the service for interacting with <see cref="EventParentModel"/> classes.
    /// </summary>
    public class EventParentService : BaseService, IEventParentService
    {
        #region Fields
        private IOnTaskDbContext context;
        private IMapperService mapper;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParentService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that the service will interact with.</param>
        /// <param name="mapper">The service which provides mappings from <see cref="EventParent"/> classes to <see cref="EventParentModel"/> classes.</param>
        public EventParentService(IOnTaskDbContext context, IMapperService mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to delete.</param>
        public void Delete(int id)
        {
            try
            {
                var entity = context.GetEventParentByIdTracked(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    context.DeleteEventParent(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets <see cref="EventParentModel"/> classes.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventParentModel"/> classes.</returns>
        public IEnumerable<EventParentModel> GetAll()
        {
            try
            {
                return context
                    .GetEventParents(ApplicationUser.Id)
                    .Select(x => mapper.Map<EventParentModel>(x))
                    .Cast<EventParentModel>()
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to get.</param>
        /// <returns>The <see cref="EventParentModel"/> class.</returns>
        public EventParentModel GetById(int id)
        {
            try
            {
                var model = default(EventParentModel);
                var entity = context.GetEventParentById(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    model = mapper.Map<EventParentModel>(entity);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventParentModel"/> class to insert.</param>
        public void Insert(EventParentModel model)
        {
            try
            {
                var entity = (EventParent)new EventParent
                {
                    UserId = ApplicationUser.Id,
                    CreatedOn = DateTime.Now
                }.InjectFrom<SmartInjection>(model);
                context.InsertEventParent(entity);
                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventParentModel"/> class to update.</param>
        public void Update(EventParentModel model)
        {
            try
            {
                var entity = context.GetEventParentByIdTracked(model.Id.Value);
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
    }
}
