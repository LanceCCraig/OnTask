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
    public class EventGroupServiceTest
    {
        #region Constants
        private const string InvalidUserId = "barbaz";
        private static User User = new User { Id = "foobar" };
        #endregion

        #region Initialization
        private static IEventGroupService InitializeTarget(IOnTaskDbContext context) =>
            InitializeTarget(context, new Mock<IMapperService>().Object);

        private static IEventGroupService InitializeTarget(IOnTaskDbContext context, IMapperService mapper)
        {
            IEventGroupService target = new EventGroupService(context, mapper);
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
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == id))).Returns<EventGroup>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEventGroup(It.IsAny<EventGroup>()), Times.Never());
        }

        [TestMethod]
        public void Delete_InvalidUserId_ContextDeleteNotCalled()
        {
            var entity = new EventGroup { Id = 1, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == entity.Id))).Returns(entity).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(entity.Id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEventGroup(It.IsAny<EventGroup>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Delete_ThrowsException_HandledByCatchBlock()
        {
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(default(int));

            contextMock.Verify();
        }

        [TestMethod]
        public void Delete_ValidIdAndUser_ContextDeleteCalled()
        {
            var entity = new EventGroup { Id = 1, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == entity.Id))).Returns(entity).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroup(It.Is<EventGroup>(y => y == entity))).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(entity.Id);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_EntitiesFound_ContextDeleteMultipleCalled()
        {
            var queryModel = new EventGroupDeleteMultipleModel { EventParentId = 1 };
            var entities = new List<EventGroup> { new EventGroup { Id = 1 }, new EventGroup { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventParentId))).Returns(entities).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroups(It.IsAny<IEnumerable<EventGroup>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_NoEntitiesFound_ContextDeleteMultipleCalled()
        {
            var queryModel = new EventGroupDeleteMultipleModel { EventParentId = default(int) };
            var entities = new List<EventGroup> { new EventGroup { Id = 1 }, new EventGroup { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventParentId))).Returns<IEnumerable<EventGroup>>(null).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroups(It.IsAny<IEnumerable<EventGroup>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);

            contextMock.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteMultiple_ThrowsException_HandledByCatchBlock()
        {
            var queryModel = new EventGroupDeleteMultipleModel { EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(queryModel);
        }

        [TestMethod]
        public void GetAll_EntitiesFound_ReturnsModels()
        {
            var queryModel = new EventGroupGetAllModel { EventParentId = 1 };
            var entities = new List<EventGroup>
            {
                new EventGroup { Id = 1, EventParentId = queryModel.EventParentId.Value },
                new EventGroup { Id = 2, EventParentId = queryModel.EventParentId.Value }
            };
            var expected = new List<EventGroupModel> { new EventGroupModel { Id = 1 }, new EventGroupModel { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventParentId))).Returns(entities).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == entities.First()), It.IsAny<string>())).Returns(expected.First()).Verifiable();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == entities.Last()), It.IsAny<string>())).Returns(expected.Last()).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetAll(queryModel).ToList();

            contextMock.Verify();
            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void GetAll_NoEntitiesFound_ReturnsEmptyList()
        {
            var queryModel = new EventGroupGetAllModel { EventParentId = default(int) };
            var entities = new List<EventGroup>();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.Is<int>(y => y == queryModel.EventParentId))).Returns(entities).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var results = target.GetAll(queryModel).ToList();

            contextMock.Verify();
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAll_ThrowsException_HandledByCatchBlock()
        {
            var queryModel = new EventGroupGetAllModel { EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.GetAll(queryModel).ToList();
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnsNull()
        {
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupById(It.IsAny<int>())).Returns<EventGroupModel>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var actual = target.GetById(default(int));

            contextMock.Verify();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetById_InvalidUserId_ReturnsNull()
        {
            int id = 1;
            var entity = new EventGroup { Id = id, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupById(It.Is<int>(y => y == id))).Returns(entity).Verifiable();
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
            contextMock.Setup(x => x.GetEventGroupById(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            var actual = target.GetById(default(int));
        }

        [TestMethod]
        public void GetById_ValidIdAndUserId_ReturnsModel()
        {
            int id = 1;
            var entity = new EventGroup { Id = id, UserId = User.Id };
            var expected = new EventGroupModel { Id = id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupById(It.Is<int>(y => y == id))).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == entity), It.IsAny<object>())).Returns(expected).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetById(id);

            contextMock.Verify();
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void Insert_ModelIsProvided_InsertsEntity()
        {
            int id = 1;
            var entity = new EventGroup { Id = id, UserId = User.Id };
            var model = new EventGroupModel();
            var insertedModel = new EventGroupModel { Id = id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEventGroup(It.IsAny<EventGroup>())).Callback<EventGroup>(x => x.Id = id).Verifiable();
            contextMock.Setup(x => x.GetEventGroupById(id)).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(entity, It.IsAny<string>())).Returns(insertedModel).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            target.Insert(model);

            contextMock.Verify();
            Assert.AreEqual(id, model.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Insert_ThrowsException_HandledByCatchBlock()
        {
            var model = new EventGroupModel();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEventGroup(It.IsAny<EventGroup>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Insert(model);
        }

        [TestMethod]
        public void Update_InvalidId_ContextSaveChangesNotCalled()
        {
            var model = new EventGroupModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == model.Id))).Returns<EventGroup>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Update_InvalidUserId_ContextSaveChangesNotCalled()
        {
            var model = new EventGroupModel { Id = 1 };
            var entity = new EventGroup { Id = model.Id.Value, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == model.Id))).Returns(entity).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Update_ThrowsException_HandledByCatchBlock()
        {
            var model = new EventGroupModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Update(model);
        }

        [TestMethod]
        public void Update_ValidIdAndUserId_ContextSaveIsCalled()
        {
            var model = new EventGroupModel { Id = 1 };
            var entity = new EventGroup { Id = model.Id.Value, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == model.Id))).Returns(entity).Verifiable();
            contextMock.Setup(x => x.SaveChanges()).Verifiable();
            contextMock.Setup(x => x.GetEventGroupById(model.Id.Value)).Returns(entity).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(entity, It.IsAny<string>())).Returns(model).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            target.Update(model);

            contextMock.Verify();
        }
        #endregion
    }
}
