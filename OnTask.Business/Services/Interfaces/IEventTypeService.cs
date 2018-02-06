using System.Collections.Generic;
using OnTask.Business.Models.Event;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes the service for interacting with <see cref="EventTypeModel"/> classes.
    /// </summary>
    public interface IEventTypeService : IBaseService
    {
        /// <summary>
        /// Deletes an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventTypeModel"/> class to delete.</param>
        void Delete(int id);
        /// <summary>
        /// Deletes multiple <see cref="EventTypeModel"/> classes.
        /// </summary>
        /// <param name="model">The model which rovides data on which <see cref="EventTypeModel"/> classes to delete.</param>
        void DeleteMultiple(EventTypeDeleteMultipleModel model);
        /// <summary>
        /// Gets <see cref="EventTypeModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventTypeModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventTypeModel"/> classes.</returns>
        IEnumerable<EventTypeModel> GetAll(EventTypeGetAllModel model);
        /// <summary>
        /// Gets the <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventTypeModel"/> class to get.</param>
        /// <returns>The <see cref="EventTypeModel"/> class.</returns>
        EventTypeModel GetById(int id);
        /// <summary>
        /// Inserts an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventTypeModel"/> class to insert.</param>
        void Insert(EventTypeModel model);
        /// <summary>
        /// Updates an <see cref="EventTypeModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventTypeModel"/> class to update.</param>
        void Update(EventTypeModel model);
    }
}