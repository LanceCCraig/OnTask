using Microsoft.EntityFrameworkCore;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnTask.Data.Contexts.Interfaces
{
    /// <summary>
    /// Exposes the <see cref="DbContext"/> for the OnTask database.
    /// </summary>
    public interface IOnTaskDbContext
    {
        #region Overall
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();
        #endregion

        #region Event
        /// <summary>
        /// Deletes an <see cref="Event"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void DeleteEvent(Event entity);
        /// <summary>
        /// Deletes multiple <see cref="Event"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void DeleteEvents(IEnumerable<Event> entities);
        /// <summary>
        /// Gets the <see cref="Event"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Event"/> class.</param>
        /// <returns>The <see cref="Event"/> class or <c>null</c> if not found.</returns>
        Event GetEventById(int id);
        /// <summary>
        /// Gets the <see cref="Event"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Event"/> class.</param>
        /// <returns>The <see cref="Event"/> class or <c>null</c> if not found.</returns>
        Event GetEventByIdTracked(int id);
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
        IEnumerable<Event> GetEvents(string userId, int? typeId, int? groupId, int? parentId, DateTime? dateRangeStart, DateTime? dateRangeEnd);
        /// <summary>
        /// Gets the <see cref="Event"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="typeId">The optional identifier of the associated <see cref="EventType"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        IEnumerable<Event> GetEventsTracked(string userId, int? typeId, int? groupId, int? parentId);
        /// <summary>
        /// Inserts an <see cref="Event"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void InsertEvent(Event entity);
        #endregion

        #region EventGroup
        /// <summary>
        /// Deletes an <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void DeleteEventGroup(EventGroup entity);
        /// <summary>
        /// Deletes multiple <see cref="EventGroup"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void DeleteEventGroups(IEnumerable<EventGroup> entities);
        /// <summary>
        /// Gets the <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventGroup"/> class to get.</param>
        /// <returns>The <see cref="EventGroup"/> class or <c>null</c> if not found.</returns>
        EventGroup GetEventGroupById(int id);
        /// <summary>
        /// Gets the <see cref="EventGroup"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventGroup"/> class to get.</param>
        /// <returns>The <see cref="EventGroup"/> class or <c>null</c> if not found.</returns>
        EventGroup GetEventGroupByIdTracked(int id);
        /// <summary>
        /// Gets the <see cref="EventGroup"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventGroup"/> classes.</returns>
        IEnumerable<EventGroup> GetEventGroups(string userId, int? parentId);
        /// <summary>
        /// Gets the <see cref="EventGroup"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="parentId">The identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventGroup"/> classes.</returns>
        IEnumerable<EventGroup> GetEventGroupsTracked(string userId, int parentId);
        /// <summary>
        /// Inserts an <see cref="EventGroup"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void InsertEventGroup(EventGroup entity);
        #endregion

        #region EventParent
        /// <summary>
        /// Deletes an <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous delete operation.</returns>
        void DeleteEventParent(EventParent entity);
        /// <summary>
        /// Gets the <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventParent"/> class.</param>
        /// <returns>The <see cref="EventParent"/> class or <c>null</c> if not found.</returns>
        EventParent GetEventParentById(int id);
        /// <summary>
        /// Gets the <see cref="EventParent"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventParent"/> class.</param>
        /// <returns>The <see cref="EventParent"/> class or <c>null</c> if not found.</returns>
        EventParent GetEventParentByIdTracked(int id);
        /// <summary>
        /// Gets the <see cref="EventParent"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        IEnumerable<EventParent> GetEventParents(string userId);
        /// <summary>
        /// Inserts an <see cref="EventParent"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void InsertEventParent(EventParent entity);
        #endregion

        #region EventType
        /// <summary>
        /// Deletes an <see cref="EventType"/> class.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void DeleteEventType(EventType entity);
        /// <summary>
        /// Deletes multiple <see cref="EventType"/> classes.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void DeleteEventTypes(IEnumerable<EventType> entities);
        /// <summary>
        /// Gets the <see cref="EventType"/> class.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventType"/> class to get.</param>
        /// <returns>The <see cref="EventType"/> class or <c>null</c> if not found.</returns>
        EventType GetEventTypeById(int id);
        /// <summary>
        /// Gets the <see cref="EventType"/> class with tracking enabled.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="EventType"/> class to get.</param>
        /// <returns>The <see cref="EventType"/> class or <c>null</c> if not found.</returns>
        EventType GetEventTypeByIdTracked(int id);
        /// <summary>
        /// Gets the <see cref="EventType"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventType"/> classes.</returns>
        IEnumerable<EventType> GetEventTypes(string userId, int? groupId, int? parentId);
        /// <summary>
        /// Gets the <see cref="EventType"/> classes by the provided filters with tracking enabled.
        /// </summary>
        /// <param name="userId">The identifier of the associated <see cref="User"/> class.</param>
        /// <param name="groupId">The optional identifier of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentId">The optional identifier of the associated <see cref="EventParent"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="EventType"/> classes.</returns>
        IEnumerable<EventType> GetEventTypesTracked(string userId, int? groupId, int? parentId);
        /// <summary>
        /// Inserts an <see cref="EventType"/> class.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void InsertEventType(EventType entity);
        #endregion
    }
}
