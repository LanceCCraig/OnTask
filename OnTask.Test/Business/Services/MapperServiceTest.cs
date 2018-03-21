using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Event;
using OnTask.Business.Services;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using static OnTask.Common.Enumerations;

namespace OnTask.Test.Business.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapperServiceTest
    {
        #region Fields
        private readonly IMapperService target = new MapperService();
        #endregion

        #region Tests
        [TestMethod]
        public void MapEvent_EntityWithAllAssociated_EverythingMapped()
        {
            var entity = new Event
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                EventTypeId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz",
                StartDate = new DateTime(2018, 01, 01),
                EndDate = new DateTime(2018, 02, 01),
                EventGroup = new EventGroup { Id = 1, Name = "foo" },
                EventParent = new EventParent { Id = 1, Name = "bar" },
                EventType = new EventType { Id = 1, Name = "baz" },
                User = new User { Id = "foo" }
            };
            var expected = new EventModel
            {
                Id = 1,
                EventGroupId = 1,
                EventGroupName = "foo",
                EventParentId = 1,
                EventParentName = "bar",
                EventTypeId = 1,
                EventTypeName = "baz",
                Name = "bar",
                Description = "baz",
                StartDate = new DateTime(2018, 01, 01),
                EndDate = new DateTime(2018, 02, 01)
            };

            var actual = target.Map<EventModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventGroupId, actual.EventGroupId);
            Assert.AreEqual(expected.EventGroupName, actual.EventGroupName);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.EventTypeId, actual.EventTypeId);
            Assert.AreEqual(expected.EventTypeName, actual.EventTypeName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
        }

        [TestMethod]
        public void MapEvent_EntityWithNoAssociated_SomeMapped()
        {
            var entity = new Event
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                EventTypeId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz",
                StartDate = new DateTime(2018, 01, 01),
                EndDate = new DateTime(2018, 02, 01)
            };
            var expected = new EventModel
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                EventTypeId = 1,
                Name = "bar",
                Description = "baz",
                StartDate = new DateTime(2018, 01, 01),
                EndDate = new DateTime(2018, 02, 01)
            };

            var actual = target.Map<EventModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventGroupId, actual.EventGroupId);
            Assert.AreEqual(expected.EventGroupName, actual.EventGroupName);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.EventTypeId, actual.EventTypeId);
            Assert.AreEqual(expected.EventTypeName, actual.EventTypeName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
        }

        [TestMethod]
        public void MapEventType_EntityWithAllAssociated_EverythingMapped()
        {
            var entity = new EventType
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz",
                EventGroup = new EventGroup { Id = 1, Name = "foo" },
                EventParent = new EventParent { Id = 1, Name = "bar" },
                User = new User { Id = "foo" }
            };
            var expected = new EventTypeModel
            {
                Id = 1,
                EventGroupId = 1,
                EventGroupName = "foo",
                EventParentId = 1,
                EventParentName = "bar",
                Name = "bar",
                Description = "baz"
            };

            var actual = target.Map<EventTypeModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventGroupId, actual.EventGroupId);
            Assert.AreEqual(expected.EventGroupName, actual.EventGroupName);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        [TestMethod]
        public void MapEventType_EntityWithNoAssociated_SomeMapped()
        {
            var entity = new EventType
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz"
            };
            var expected = new EventTypeModel
            {
                Id = 1,
                EventGroupId = 1,
                EventParentId = 1,
                Name = "bar",
                Description = "baz"
            };

            var actual = target.Map<EventTypeModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventGroupId, actual.EventGroupId);
            Assert.AreEqual(expected.EventGroupName, actual.EventGroupName);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        [TestMethod]
        public void MapEventGroup_EntityWithAllAssociated_EverythingMapped()
        {
            var entity = new EventGroup
            {
                Id = 1,
                EventParentId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz",
                EventParent = new EventParent { Id = 1, Name = "foo" },
                User = new User { Id = "foo" }
            };
            var expected = new EventGroupModel
            {
                Id = 1,
                EventParentId = 1,
                EventParentName = "foo",
                Name = "bar",
                Description = "baz"
            };

            var actual = target.Map<EventGroupModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        [TestMethod]
        public void MapEventGroup_EntityWithNoAssociated_SomeMapped()
        {
            var entity = new EventGroup
            {
                Id = 1,
                EventParentId = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz"
            };
            var expected = new EventGroupModel
            {
                Id = 1,
                EventParentId = 1,
                Name = "bar",
                Description = "baz"
            };

            var actual = target.Map<EventGroupModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventParentId, actual.EventParentId);
            Assert.AreEqual(expected.EventParentName, actual.EventParentName);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        [TestMethod]
        public void MapEventParent_EntityWithAllAssociated_EverythingMapped()
        {
            var entity = new EventParent
            {
                Id = 1,
                UserId = "foo",
                Name = "bar",
                Description = "baz",
                User = new User { Id = "foo" }
            };
            var expected = new EventParentModel
            {
                Id = 1,
                Name = "bar",
                Description = "baz"
            };

            var actual = target.Map<EventParentModel>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
        }
        #endregion
    }
}
