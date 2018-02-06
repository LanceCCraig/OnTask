using System.Collections.Generic;
using OnTask.Business.Models.Event;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes the service for interacting with <see cref="EventGroupModel"/> classes.
    /// </summary>
    public interface IEventGroupService : IBaseService
    {
        /// <summary>
        /// Deletes an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventGroupModel"/> class to delete.</param>
        void Delete(int id);
        /// <summary>
        /// Deletes multiple <see cref="EventGroupModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventGroupModel"/> classes to delete.</param>
        void DeleteMultiple(EventGroupDeleteMultipleModel model);
        /// <summary>
        /// Gets <see cref="EventGroupModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventGroupModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventGroupModel"/> classes.</returns>
        IEnumerable<EventGroupModel> GetAll(EventGroupGetAllModel model);
        /// <summary>
        /// Gets the <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventGroupModel"/> class to get.</param>
        /// <returns>The <see cref="EventGroupModel"/> class.</returns>
        EventGroupModel GetById(int id);
        /// <summary>
        /// Inserts an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventGroupModel"/> class to insert.</param>
        void Insert(EventGroupModel model);
        /// <summary>
        /// Updates an <see cref="EventGroupModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventGroupModel"/> class to update.</param>
        void Update(EventGroupModel model);
    }
}