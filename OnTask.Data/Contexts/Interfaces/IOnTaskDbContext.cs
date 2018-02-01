using Microsoft.EntityFrameworkCore;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnTask.Data.Contexts.Interfaces
{
    /// <summary>
    /// Exposes the <see cref="DbContext"/> for the OnTask database.
    /// </summary>
    public interface IOnTaskDbContext
    {
        /// <summary>
        /// Gets the <see cref="Event"/> classes by the provided filters.
        /// </summary>
        /// <param name="userId">The ID of the associated <see cref="User"/> class.</param>
        /// <param name="typeName">The optional name of the associated <see cref="EventType"/> class.</param>
        /// <param name="groupName">The optional name of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentName">The optional name of the associated <see cref="EventParent"/> class.</param>
        /// <param name="dateRangeStart">The optional minimum <see cref="Event.StartDate"/>.</param>
        /// <param name="dateRangeEnd">The optional maximum <see cref="Event.StartDate"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        Task<IEnumerable<Event>> GetEvents(string userId, string typeName, string groupName, string parentName, DateTime? dateRangeStart, DateTime? dateRangeEnd);
    }
}
