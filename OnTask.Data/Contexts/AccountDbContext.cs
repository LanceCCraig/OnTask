using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;

namespace OnTask.Data.Contexts
{
    /// <summary>
    /// Provides the <see cref="DbContext"/> for the ASP.NET identity data.
    /// </summary>
    public class AccountDbContext : IdentityDbContext, IAccountDbContext
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDbContext"/> class.
        /// </summary>
        /// <param name="options">Provides the initialization options.</param>
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Configures the defined entities for the <see cref="AccountDbContext"/> class.
        /// </summary>
        /// <param name="modelBuilder">The builder class used for configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable(nameof(Role));
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
