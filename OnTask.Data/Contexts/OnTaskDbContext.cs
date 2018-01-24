using Microsoft.EntityFrameworkCore;
using OnTask.Common;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTask.Data.Contexts
{
    /// <summary>
    /// Provides the <see cref="DbContext"/> for the OnTask database.
    /// </summary>
    public class OnTaskDbContext : DbContext, IOnTaskDbContext
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="OnTaskDbContext"/> class.
        /// </summary>
        /// <param name="options">Provides the initialization options.</param>
        public OnTaskDbContext(DbContextOptions<OnTaskDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="Event"/> classes.
        /// </summary>
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventGroup"/> classes.
        /// </summary>
        public DbSet<EventGroup> EventGroups { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventParent"/> classes.
        /// </summary>
        public DbSet<EventParent> EventParents { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventType"/> classes.
        /// </summary>
        public DbSet<EventType> EventTypes { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Configures the defined entities for the <see cref="OnTaskDbContext"/> class.
        /// </summary>
        /// <param name="modelBuilder">The builder class used for configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable(nameof(Event));
            modelBuilder.Entity<EventGroup>().ToTable(nameof(EventGroup));
            modelBuilder.Entity<EventType>().ToTable(nameof(EventType));
            modelBuilder.Entity<EventParent>().ToTable(nameof(EventParent));
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets the <see cref="Event"/> classes by the provided filters.
        /// </summary>
        /// <param name="groupName">The optional name of the associated <see cref="EventGroup"/> class.</param>
        /// <param name="parentName">The optional name of the associated <see cref="EventParent"/> class.</param>
        /// <param name="typeName">The optional name of the associated <see cref="EventType"/> class.</param>
        /// <param name="userId">The ID of the associated <see cref="User"/> class.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of all matching <see cref="Event"/> classes.</returns>
        public async Task<IEnumerable<Event>> GetEvents(
            string groupName,
            string parentName,
            string typeName,
            string userId) => await Events
            .Include(x => x.EventGroup)
            .Include(x => x.EventParent)
            .Include(x => x.EventType)
            .Where(x =>
                x.EventGroup.Name.IsParameterNullOrEqual(groupName) &&
                x.EventParent.Name.IsParameterNullOrEqual(parentName) &&
                x.EventType.Name.IsParameterNullOrEqual(typeName) &&
                x.UserId == userId)
            .ToListAsync();
        #endregion
    }
}
