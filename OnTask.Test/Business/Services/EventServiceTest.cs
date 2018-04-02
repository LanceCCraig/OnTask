using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnTask.Business.Models.Event;
using OnTask.Business.Services;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OnTask.Test.Business.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventServiceTest
    {
        #region Constants
        private const string InvalidUserId = "barbaz";
        private static User User = new User { Id = "foobar" };
        #endregion

        #region Initialization
        private static IEventService InitializeTarget(IOnTaskDbContext context) =>
            InitializeTarget(context, new Mock<IMapperService>().Object);

        private static IEventService InitializeTarget(IOnTaskDbContext context, IMapperService mapper)
        {
            IEventService target = new EventService(context, mapper);
            target.AddApplicationUser(User);
            return target;
        }
        #endregion

        #region Tests
        [TestMethod]
        public void Delete_InvalidId_ContextDeleteNotCalled()
        {
            int id = 1;
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == id))).Returns<Event>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEvent(It.IsAny<Event>()), Times.Never());
        }

        [TestMethod]
        public void Delete_InvalidUserId_ContextDeleteNotCalled()
        {
            var entity = new Event { Id = 1, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == entity.Id))).Returns(entity).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(entity.Id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEvent(It.IsAny<Event>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Delete_ThrowsException_HandledByCatchBlock()
        {
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(default(int));

            contextMock.Verify();
        }

        [TestMethod]
        public void Delete_ValidIdAndUser_ContextDeleteCalled()
        {
            var entity = new Event { Id = 1, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == entity.Id))).Returns(entity).Verifiable();
            contextMock.Setup(x => x.DeleteEvent(It.Is<Event>(y => y == entity))).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(entity.Id);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_EntitiesFound_ContextDeleteMultipleCalled()
        {
            var queryModel = new EventDeleteMultipleModel { EventTypeId = 1, EventGroupId = 1, EventParentId = 1 };
            var entities = new List<Event> { new Event { Id = 1 }, new Event { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventsTracked(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventTypeId), It.Is<int>(y => y == queryModel.EventGroupId), It.Is<int>(y => y == queryModel.EventParentId))).Returns(entities).Verifiable();
            contextMock.Setup(x => x.DeleteEvents(It.IsAny<IEnumerable<Event>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_NoEntitiesFound_ContextDeleteMultipleCalled()
        {
            var queryModel = new EventDeleteMultipleModel { EventTypeId = default(int), EventGroupId = default(int), EventParentId = default(int) };
            var entities = new List<Event> { new Event { Id = 1 }, new Event { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventsTracked(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventTypeId), It.Is<int>(y => y == queryModel.EventGroupId), It.Is<int>(y => y == queryModel.EventParentId))).Returns<IEnumerable<EventGroup>>(null).Verifiable();
            contextMock.Setup(x => x.DeleteEvents(It.IsAny<IEnumerable<Event>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);

            contextMock.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteMultiple_ThrowsException_HandledByCatchBlock()
        {
            var queryModel = new EventDeleteMultipleModel { EventTypeId = default(int), EventGroupId = default(int), EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventsTracked(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);
        }

        [TestMethod]
        public void GetAll_EntitiesFound_ReturnsModels()
        {
            var queryModel = new EventGetAllModel { EventTypeId = 1, EventGroupId = 1, EventParentId = 1 };
            var entities = new List<Event>
            {
                new Event { Id = 1, EventTypeId = queryModel.EventTypeId.Value, EventGroupId = queryModel.EventGroupId.Value, EventParentId = queryModel.EventParentId.Value },
                new Event { Id = 2, EventTypeId = queryModel.EventTypeId.Value, EventGroupId = queryModel.EventGroupId.Value, EventParentId = queryModel.EventParentId.Value }
            };
            var expected = new List<EventModel> { new EventModel { Id = 1 }, new EventModel { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock
                .Setup(x => x.GetEvents(
                    It.IsAny<string>(),
                    It.Is<int>(y => y == queryModel.EventTypeId),
                    It.Is<int>(y => y == queryModel.EventGroupId),
                    It.Is<int>(y => y == queryModel.EventParentId),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>())).Returns(entities).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventModel>(It.Is<Event>(y => y == entities.First()), It.IsAny<string>())).Returns(expected.First()).Verifiable();
            mapperMock.Setup(x => x.Map<EventModel>(It.Is<Event>(y => y == entities.Last()), It.IsAny<string>())).Returns(expected.Last()).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetAll(queryModel).ToList();

            contextMock.Verify();
            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void GetAll_NoEntitiesFound_ReturnsEmptyList()
        {
            var queryModel = new EventGetAllModel { EventTypeId = default(int), EventGroupId = default(int), EventParentId = default(int) };
            var entities = new List<Event>();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEvents(
                    It.IsAny<string>(),
                    It.Is<int>(y => y == queryModel.EventTypeId),
                    It.Is<int>(y => y == queryModel.EventGroupId),
                    It.Is<int>(y => y == queryModel.EventParentId),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>())).Returns(entities).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var results = target.GetAll(queryModel).ToList();

            contextMock.Verify();
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAll_ThrowsException_HandledByCatchBlock()
        {
            var queryModel = new EventGetAllModel { EventTypeId = default(int), EventGroupId = default(int), EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEvents(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.GetAll(queryModel).ToList();
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnsNull()
        {
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventById(It.IsAny<int>())).Returns<EventTypeModel>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var actual = target.GetById(default(int));

            contextMock.Verify();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetById_InvalidUserId_ReturnsNull()
        {
            int id = 1;
            var entity = new Event { Id = id, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventById(It.Is<int>(y => y == id))).Returns(entity).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var actual = target.GetById(id);

            contextMock.Verify();
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetById_ThrowsException_HandledByCatchBlock()
        {
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventById(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            var actual = target.GetById(default(int));
        }

        [TestMethod]
        public void GetById_ValidIdAndUserId_ReturnsModel()
        {
            int id = 1;
            var entity = new Event { Id = id, UserId = User.Id };
            var expected = new EventModel { Id = id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventById(It.Is<int>(y => y == id))).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventModel>(It.Is<Event>(y => y == entity), It.IsAny<object>())).Returns(expected).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetById(id);

            contextMock.Verify();
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void Insert_ModelIsProvided_InsertsEntity()
        {
            int id = 1;
            var model = new EventModel();
            var entity = new Event { Id = id, UserId = User.Id };
            var insertedModel = new EventModel { Id = id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEvent(It.IsAny<Event>())).Callback<Event>(x => x.Id = id).Verifiable();
            contextMock.Setup(x => x.GetEventById(id)).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventModel>(entity, It.IsAny<string>())).Returns(insertedModel).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            target.Insert(model);

            contextMock.Verify();
            Assert.AreEqual(id, model.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Insert_ThrowsException_HandledByCatchBlock()
        {
            var model = new EventModel();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEvent(It.IsAny<Event>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Insert(model);
        }

        [TestMethod]
        public void Update_InvalidId_ContextSaveChangesNotCalled()
        {
            var model = new EventModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == model.Id))).Returns<Event>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Update_InvalidUserId_ContextSaveChangesNotCalled()
        {
            var model = new EventModel { Id = 1 };
            var entity = new Event { Id = model.Id.Value, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == model.Id))).Returns(entity).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Update_ThrowsException_HandledByCatchBlock()
        {
            var model = new EventModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);
        }

        [TestMethod]
        public void Update_ValidIdAndUserId_ContextSaveIsCalled()
        {
            var model = new EventModel { Id = 1 };
            var entity = new Event { Id = model.Id.Value, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventByIdTracked(It.Is<int>(y => y == model.Id))).Returns(entity).Verifiable();
            contextMock.Setup(x => x.SaveChanges()).Verifiable();
            contextMock.Setup(x => x.GetEventById(model.Id.Value)).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventModel>(entity, It.IsAny<string>())).Returns(model).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            target.Update(model);

            contextMock.Verify();
        }
        #endregion
    }
}
