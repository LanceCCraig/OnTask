using Microsoft.EntityFrameworkCore;
using OnTask.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnTask.Data.Contexts
{
    public partial class OnTaskDbContext
    {
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventParent"/> classes.
        /// </summary>
        public DbSet<EventParent> EventParents { get; set; }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void DeleteEventParent(EventParent entity)
        {
            EventParents.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Gets the <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventParent"/> class.</param>
        /// <returns>The <see cref="EventParent"/> class or <c>null</c> if not found.</returns>
        public EventParent GetEventParentById(int id) => EventParents
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventParent"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventParent"/> class.</param>
        /// <returns>The <see cref="EventParent"/> class or <c>null</c> if not found.</returns>
        public EventParent GetEventParentByIdTracked(int id) => EventParents.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventParent"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventParent"/> classes.</returns>
        public IEnumerable<EventParent> GetEventParents(string userId) => EventParents
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToList();

        /// <summary>
        /// Inserts an <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public void InsertEventParent(EventParent entity)
        {
            EventParents.Add(entity);
            SaveChanges();
        }
        #endregion
    }
}
