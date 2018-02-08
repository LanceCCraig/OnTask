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
    /// Provides the service for interacting with <see cref="EventGroupModel"/> classes.
    /// </summary>
    public class EventGroupService : BaseService, IEventGroupService
    {
        #region Fields
        private IOnTaskDbContext context;
        private IMapperService mapper;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventGroupService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> that the service will interact with.</param>
        /// <param name="mapper">The service which provides mappings from <see cref="EventGroup"/> classes to <see cref="EventGroupModel"/> classes.</param>
        public EventGroupService(IOnTaskDbContext context, IMapperService mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventGroupModel"/> class to delete.</param>
        public void Delete(int id)
        {
            try
            {
                var entity = context.GetEventGroupByIdTracked(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    context.DeleteEventGroup(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes multiple <see cref="EventGroupModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventGroupModel"/> classes to delete.</param>
        public void DeleteMultiple(EventGroupDeleteMultipleModel model)
        {
            try
            {
                context.DeleteEventGroups(context.GetEventGroupsTracked(ApplicationUser.Id, model.EventParentId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets <see cref="EventGroupModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventGroupModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventGroupModel"/> classes.</returns>
        public IEnumerable<EventGroupModel> GetAll(EventGroupGetAllModel model)
        {
            try
            {
                return context
                    .GetEventGroups(
                        ApplicationUser.Id,
                        model.EventParentId)
                    .Select(x => mapper.Map<EventGroupModel>(x))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventGroupModel"/> class to get.</param>
        /// <returns>The <see cref="EventGroupModel"/> class.</returns>
        public EventGroupModel GetById(int id)
        {
            try
            {
                var model = default(EventGroupModel);
                var entity = context.GetEventGroupById(id);
                if (entity != null &&
                    entity.UserId == ApplicationUser.Id)
                {
                    model = mapper.Map<EventGroupModel>(entity);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventGroupModel"/> class to insert.</param>
        public void Insert(EventGroupModel model)
        {
            try
            {
                var entity = (EventGroup)new EventGroup
                {
                    UserId = ApplicationUser.Id,
                    CreatedOn = DateTime.Now
                }.InjectFrom<SmartInjection>(model);
                context.InsertEventGroup(entity);
                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventGroupModel"/> class to update.</param>
        public void Update(EventGroupModel model)
        {
            try
            {
                var entity = context.GetEventGroupByIdTracked(model.Id.Value);
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
