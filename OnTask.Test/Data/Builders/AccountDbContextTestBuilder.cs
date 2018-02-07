using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnTask.Data.Contexts;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Data.Builders
{
    [ExcludeFromCodeCoverage]
    public class AccountDbContextTestBuilder
    {
        #region Fields
        private AccountDbContext context;
        #endregion

        #region Public Interface
        public AccountDbContextTestBuilder AddRoles(IEnumerable<Role> entities) =>
            SaveAndReturnBuilder(() => context.Roles.AddRange(entities));

        public AccountDbContextTestBuilder AddRoleClaims(IEnumerable<IdentityRoleClaim<string>> entities) =>
            SaveAndReturnBuilder(() => context.RoleClaims.AddRange(entities));

        public AccountDbContextTestBuilder AddUsers(IEnumerable<User> entities) =>
            SaveAndReturnBuilder(() => context.Users.AddRange(entities));

        public AccountDbContextTestBuilder AddUserClaims(IEnumerable<IdentityUserClaim<string>> entities) =>
            SaveAndReturnBuilder(() => context.UserClaims.AddRange(entities));

        public AccountDbContextTestBuilder AddUserLogins(IEnumerable<IdentityUserLogin<string>> entities) =>
            SaveAndReturnBuilder(() => context.UserLogins.AddRange(entities));

        public AccountDbContextTestBuilder AddUserRoles(IEnumerable<IdentityUserRole<string>> entities) =>
            SaveAndReturnBuilder(() => context.UserRoles.AddRange(entities));

        public AccountDbContextTestBuilder AddUserTokens(IEnumerable<IdentityUserToken<string>> entities) =>
            SaveAndReturnBuilder(() => context.UserTokens.AddRange(entities));

        public IAccountDbContext Build() => context;

        public AccountDbContextTestBuilder Create()
        {
            context = new AccountDbContext(new DbContextOptionsBuilder<AccountDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider())
                .Options);
            return this;
        }
        #endregion

        #region Private Helpers
        private AccountDbContextTestBuilder SaveAndReturnBuilder(Action action)
        {
            action();
            context.SaveChanges();
            return this;
        }
        #endregion
    }
}
