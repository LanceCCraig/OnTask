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
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventType"/> classes.
        /// </summary>
        public DbSet<EventType> EventTypes { get; set; }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="EventType"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void DeleteEventType(EventType entity)
        {
            EventTypes.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Deletes multiple <see cref="EventType"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void DeleteEventTypes(IEnumerable<EventType> entities)
        {
            EventTypes.RemoveRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// Gets the <see cref="EventType"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventType"/> class to get.</param>
        /// <returns>The <see cref="EventType"/> class or <c>null</c> if not found.</returns>
        public EventType GetEventTypeById(int id) => EventTypes
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventType"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventType"/> class to get.</param>
        /// <returns>The <see cref="EventType"/> class or <c>null</c> if not found.</returns>
        public EventType GetEventTypeByIdTracked(int id) => EventTypes.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the <see cref="EventType"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventType"/> classes.</returns>
        public IEnumerable<EventType> GetEventTypes(
            string userId,
            int? groupId,
            int? parentId) => EventTypes
            .AsNoTracking()
            .Include(x => x.EventGroup)
            .Include(x => x.EventParent)
            .Where(x =>
                x.UserId == userId &&
                x.EventGroupId.IsParameterNullOrEqualForNonNullable(groupId) &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId))
            .ToList();

        /// <summary>
        /// Gets the <see cref="EventType"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventType"/> classes.</returns>
        public IEnumerable<EventType> GetEventTypesTracked(
            string userId,
            int? groupId,
            int? parentId) => EventTypes
            .Where(x =>
                x.UserId == userId &&
                x.EventGroupId.IsParameterNullOrEqualForNonNullable(groupId) &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId))
            .ToList();

        /// <summary>
        /// Inserts an <see cref="EventType"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public void InsertEventType(EventType entity)
        {
            EventTypes.Add(entity);
            SaveChanges();
        }
        #endregion
    }
}
