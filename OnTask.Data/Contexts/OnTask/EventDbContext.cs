using Microsoft.EntityFrameworkCore;
using OnTask.Common;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTask.Data.Contexts
{
    public partial class OnTaskDbContext
    {
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="Event"/> classes.
        /// </summary>
        public DbSet<Event> Events { get; set; }
        #endregion

        #region Public Interface
        /// <summary>
        /// Deletes an <see cref="Event"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void DeleteEvent(Event entity)
        {
            Events.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Deletes multiple <see cref="Event"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void DeleteEvents(IEnumerable<Event> entities)
        {
            Events.RemoveRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// Gets the <see cref="Event"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Event"/> class to get.</param>
        /// <returns>The <see cref="Event"/> class or <c>null</c> if not found.</returns>
        public Event GetEventById(int id) => Events
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        /// <summary>
        /// Gets the <see cref="Event"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Event"/> class to get.</param>
        /// <returns>The <see cref="Event"/> class or <c>null</c> if not found.</returns>
        public Event GetEventByIdTracked(int id) => Events.FirstOrDefault(x => x.Id == id);
            
        /// <summary>
        /// Gets the <see cref="Event"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="typeId">The optional identifier of the associated <see cref="EventType"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <param name="dateRangeStart">The optional minimum <see cref="Event.StartDate"/>.</param>
        /// <param name="dateRangeEnd">The optional maximum <see cref="Event.StartDate"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        public IEnumerable<Event> GetEvents(
            string userId,
            int? typeId,
            int? groupId,
            int? parentId,
            DateTime? dateRangeStart,
            DateTime? dateRangeEnd) => Events
            .AsNoTracking()
            .Include(x => x.EventGroup)
            .Include(x => x.EventParent)
            .Include(x => x.EventType)
            .Where(x =>
                x.UserId == userId &&
                x.EventTypeId.IsParameterNullOrEqualForNonNullable(typeId) &&
                x.EventGroupId.IsParameterNullOrEqualForNonNullable(groupId) &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId) &&
                (!dateRangeStart.HasValue || x.StartDate >= dateRangeStart.Value) &&
                (!dateRangeEnd.HasValue || x.StartDate <= dateRangeStart.Value))
            .ToList();

        /// <summary>
        /// Gets the <see cref="Event"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="typeId">The optional identifier of the associated <see cref="EventType"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        public IEnumerable<Event> GetEventsTracked(
            string userId,
            int? typeId,
            int? groupId,
            int? parentId) => Events
            .Where(x =>
                x.UserId == userId &&
                x.EventTypeId.IsParameterNullOrEqualForNonNullable(typeId) &&
                x.EventGroupId.IsParameterNullOrEqualForNonNullable(groupId) &&
                x.EventParentId.IsParameterNullOrEqualForNonNullable(parentId))
            .ToList();

        /// <summary>
        /// Inserts an <see cref="Event"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public void InsertEvent(Event entity)
        {
            Events.Add(entity);
            SaveChanges();
        }
        #endregion
    }
}
