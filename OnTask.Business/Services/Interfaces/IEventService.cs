using OnTask.Business.Models.Event;
using System.Collections.Generic;

namespace OnTask.Business.Services.Interfaces
{
    /// <summary>
    /// Exposes the service for interacting with <see cref="EventModel"/> classes.
    /// </summary>
    public interface IEventService : IBaseService
    {
        /// <summary>
        /// Deletes an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to delete.</param>
        void Delete(int id);
        /// <summary>
        /// Deletes multiple <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventModel"/> classes to delete.</param>
        void DeleteMultiple(EventDeleteMultipleModel model);
        /// <summary>
        /// Gets <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The model which provides data on which <see cref="EventModel"/> classes to get.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventModel"/> classes.</returns>
        IEnumerable<EventModel> GetAll(EventGetAllModel model);
        /// <summary>
        /// Gets the <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="id">The identifier for the <see cref="EventModel"/> class to get.</param>
        /// <returns>The <see cref="EventModel"/> class.</returns>
        EventModel GetById(int id);
        /// <summary>
        /// Inserts an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventModel"/> class to insert.</param>
        void Insert(EventModel model);
        /// <summary>
        /// Inserts recurring <see cref="EventModel"/> classes.
        /// </summary>
        /// <param name="model">The <see cref="RecurringEventModel"/> class that specifies the <see cref="EventModel"/> classes to insert.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all <see cref="EventModel"/> classes that were inserted.</returns>
        IEnumerable<EventModel> InsertRecurring(RecurringEventModel model);
        /// <summary>
        /// Updates an <see cref="EventModel"/> class.
        /// </summary>
        /// <param name="model">The <see cref="EventModel"/> class to update.</param>
        void Update(EventModel model);
    }
}
