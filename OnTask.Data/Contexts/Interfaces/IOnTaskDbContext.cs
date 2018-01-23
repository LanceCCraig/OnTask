using Microsoft.EntityFrameworkCore;
using OnTask.Data.Entities;
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
        /// <param name="groupName">The name of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentName">The name of the associated <see cref="EventParent"/> class.</param>
        /// <param name="typeName">The name of the associated <see cref="EventType"/> class.</param>
        /// <param name="userId">The ID of the associated <see cref="User"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        Task<IEnumerable<Event>> GetEvents(string groupName, string parentName, string typeName, string userId);
    }
}
