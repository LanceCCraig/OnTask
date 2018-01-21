using Microsoft.EntityFrameworkCore;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;

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
        private DbSet<Event> Events { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventParent"/> classes.
        /// </summary>
        private DbSet<EventParent> EventParents { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of all <see cref="EventType"/> classes.
        /// </summary>
        private DbSet<EventType> EventTypes { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Configures the defined entities for the <see cref="OnTaskDbContext"/> class.
        /// </summary>
        /// <param name="modelBuilder">The builder class used for configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable(nameof(Event));
            modelBuilder.Entity<EventType>().ToTable(nameof(EventType));
            modelBuilder.Entity<EventParent>().ToTable(nameof(EventParent));
        }
        #endregion
    }
}
