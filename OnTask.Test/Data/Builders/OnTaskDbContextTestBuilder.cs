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
    public class OnTaskDbContextTestBuilder
    {
        #region Fields
        private OnTaskDbContext context;
        #endregion

        #region Public Interface
        public OnTaskDbContextTestBuilder AddEvents(IEnumerable<Event> entities) =>
            SaveAndReturnBuilder(() => context.Events.AddRange(entities));

        public OnTaskDbContextTestBuilder AddEventGroups(IEnumerable<EventGroup> entities) =>
            SaveAndReturnBuilder(() => context.EventGroups.AddRange(entities));

        public OnTaskDbContextTestBuilder AddEventParents(IEnumerable<EventParent> entities) =>
            SaveAndReturnBuilder(() => context.EventParents.AddRange(entities));

        public OnTaskDbContextTestBuilder AddEventTypes(IEnumerable<EventType> entities) =>
            SaveAndReturnBuilder(() => context.EventTypes.AddRange(entities));

        public IOnTaskDbContext Build() => context;

        public OnTaskDbContextTestBuilder Create()
        {
            context = new OnTaskDbContext(new DbContextOptionsBuilder<OnTaskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider())
                .Options);
            return this;
        }
        #endregion

        #region Private Helpers
        private OnTaskDbContextTestBuilder SaveAndReturnBuilder(Action action)
        {
            action();
            context.SaveChanges();
            return this;
        }
        #endregion
    }
}
