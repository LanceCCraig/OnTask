using System.Collections.Generic;
using OnTask.Business.Models.Event;
using static OnTask.Common.Enumerations;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes the service for interacting with <see cref="EventParentModel"/> classes.
    /// </summary>
    public interface IEventParentService : IBaseService
    {
        /// <summary>
        /// Deletes an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to delete.</param>
        void Delete(int id);
        /// <summary>
        /// Gets <see cref="EventParentModel"/> classes.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventParentModel"/> classes.</returns>
        IEnumerable<EventParentModel> GetAll();
        /// <summary>
        /// Gets the <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventParentModel"/> class to get.</param>
        /// <returns>The <see cref="EventParentModel"/> class.</returns>
        EventParentModel GetById(int id);
        /// <summary>
        /// Inserts an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventParentModel"/> class to insert.</param>
        void Insert(EventParentModel model);
        /// <summary>
        /// Updates an <see cref="EventParentModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventParentModel"/> class to update.</param>
        void Update(EventParentModel model);
    }
}