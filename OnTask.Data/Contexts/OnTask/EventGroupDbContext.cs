using Microsoft.EntityFrameworkCore;
using OnTask.Common;
using OnTask.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnTask.Data.Contexts
{
    public partial class OnTaskDbContext
    {
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventGroup"/> classes.
        /// </summary>
        public DbSet<EventGroup> EventGroups { get; set; }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void DeleteEventGroup(EventGroup entity)
        {
            EventGroups.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Deletes multiple <see cref="EventGroup"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void DeleteEventGroups(IEnumerable<EventGroup> entities)
        {
            EventGroups.RemoveRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// Gets the <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventGroup"/> class to get.</param>
        /// <returns>The <see cref="EventGroup"/> class or <c>null</c> if not found.</returns>
        public EventGroup GetEventGroupById(int id) => EventGroups
            .AsNoTracking()
            .Include(x => x.EventParent)
            .FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventGroup"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventGroup"/> class to get.</param>
        /// <returns>The <see cref="EventGroup"/> class or <c>null</c> if not found.</returns>
        public EventGroup GetEventGroupByIdTracked(int id) => EventGroups.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventGroup"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventGroup"/> classes.</returns>
        public IEnumerable<EventGroup> GetEventGroups(
            string userId,
            int? parentId) => EventGroups
            .AsNoTracking()
            .Include(x => x.EventParent)
            .Where(x =>
                x.UserId == userId &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId))
            .ToList();

        /// <summary>
        /// Gets the <see cref="EventGroup"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="parentId">The identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventGroup"/> classes.</returns>
        public IEnumerable<EventGroup> GetEventGroupsTracked(
            string userId,
            int parentId) => EventGroups
            .Where(x =>
                x.UserId == userId &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId))
            .ToList();

        /// <summary>
        /// Inserts an <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public void InsertEventGroup(EventGroup entity)
        {
            EventGroups.Add(entity);
            SaveChanges();
        }
        #endregion
    }
}
