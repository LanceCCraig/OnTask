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
            var eventGroup = new EventGroup { Id = 1, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroup.Id))).Returns(eventGroup).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(eventGroup.Id);

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
            var eventGroup = new EventGroup { Id = 1, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroup.Id))).Returns(eventGroup).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroup(It.Is<EventGroup>(y => y == eventGroup))).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(eventGroup.Id);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_EventGroupsFound_ContextDeleteMultipleCalled()
        {
            var eventGroupDeleteMultipleModel = new EventGroupDeleteMultipleModel { EventParentId = 1 };
            var eventGroups = new List<EventGroup> { new EventGroup { Id = 1 }, new EventGroup { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.Is<int>(y => y == eventGroupDeleteMultipleModel.EventParentId))).Returns(eventGroups).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroups(It.IsAny<IEnumerable<EventGroup>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(eventGroupDeleteMultipleModel);

            contextMock.Verify();
        }

        [TestMethod]
        public void DeleteMultiple_NoEventGroupsFound_ContextDeleteMultipleCalled()
        {
            var eventGroupDeleteMultipleModel = new EventGroupDeleteMultipleModel { EventParentId = default(int) };
            var eventGroups = new List<EventGroup> { new EventGroup { Id = 1 }, new EventGroup { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.Is<int>(y => y == eventGroupDeleteMultipleModel.EventParentId))).Returns<IEnumerable<EventGroup>>(null).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroups(It.IsAny<IEnumerable<EventGroup>>())).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(eventGroupDeleteMultipleModel);

            contextMock.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteMultiple_ThrowsException_HandledByCatchBlock()
        {
            var eventGroupDeleteMultipleModel = new EventGroupDeleteMultipleModel { EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupsTracked(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.DeleteMultiple(eventGroupDeleteMultipleModel);
        }

        [TestMethod]
        public void GetAll_EventGroupsFound_ReturnsModels()
        {
            var eventGroupGetAllModel = new EventGroupGetAllModel { EventParentId = 1 };
            var eventGroups = new List<EventGroup>
            {
                new EventGroup { Id = 1, EventParentId = eventGroupGetAllModel.EventParentId.Value },
                new EventGroup { Id = 2, EventParentId = eventGroupGetAllModel.EventParentId.Value }
            };
            var expected = new List<EventGroupModel> { new EventGroupModel { Id = 1 }, new EventGroupModel { Id = 2 } };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.Is<int>(y => y == eventGroupGetAllModel.EventParentId))).Returns(eventGroups).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == eventGroups.First()), It.IsAny<string>())).Returns(expected.First()).Verifiable();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == eventGroups.Last()), It.IsAny<string>())).Returns(expected.Last()).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetAll(eventGroupGetAllModel).ToList();

            contextMock.Verify();
            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void GetAll_NoEventGroupsFound_ReturnsEmptyList()
        {
            var eventGroupGetAllModel = new EventGroupGetAllModel { EventParentId = default(int) };
            var eventGroups = new List<EventGroup>();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.Is<int>(y => y == eventGroupGetAllModel.EventParentId))).Returns(eventGroups).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            var results = target.GetAll(eventGroupGetAllModel).ToList();

            contextMock.Verify();
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAll_ThrowsException_HandledByCatchBlock()
        {
            var eventGroupGetAllModel = new EventGroupGetAllModel { EventParentId = default(int) };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroups(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.GetAll(eventGroupGetAllModel).ToList();
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
            var eventGroup = new EventGroup { Id = id, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupById(It.Is<int>(y => y == id))).Returns(eventGroup).Verifiable();
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
            var eventGroup = new EventGroup { Id = id, UserId = User.Id };
            var expected = new EventGroupModel { Id = id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupById(It.Is<int>(y => y == id))).Returns(eventGroup).Verifiable();
            var mapperMock = new Mock<IMapperService>();
            mapperMock.Setup(x => x.Map<EventGroupModel>(It.Is<EventGroup>(y => y == eventGroup), It.IsAny<object>())).Returns(expected).Verifiable();
            var target = InitializeTarget(contextMock.Object, mapperMock.Object);

            var actual = target.GetById(id);

            contextMock.Verify();
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void Insert_ModelIsProvided_InsertsEntity()
        {
            int id = 1;
            var eventGroupModel = new EventGroupModel();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEventGroup(It.IsAny<EventGroup>())).Callback<EventGroup>(x => x.Id = id).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Insert(eventGroupModel);

            contextMock.Verify();
            Assert.AreEqual(id, eventGroupModel.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Insert_ThrowsException_HandledByCatchBlock()
        {
            var eventGroupModel = new EventGroupModel();
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.InsertEventGroup(It.IsAny<EventGroup>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Insert(eventGroupModel);
        }

        [TestMethod]
        public void Update_InvalidId_ContextSaveChangesNotCalled()
        {
            var eventGroupModel = new EventGroupModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroupModel.Id))).Returns<EventGroup>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(eventGroupModel);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void Update_InvalidUserId_ContextSaveChangesNotCalled()
        {
            var eventGroupModel = new EventGroupModel { Id = 1 };
            var eventGroup = new EventGroup { Id = eventGroupModel.Id.Value, UserId = InvalidUserId };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroupModel.Id))).Returns(eventGroup).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(eventGroupModel);

            contextMock.Verify();
            contextMock.Verify(x => x.SaveChanges(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Update_ThrowsException_HandledByCatchBlock()
        {
            var eventGroupModel = new EventGroupModel { Id = 1 };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.IsAny<int>())).Throws<Exception>();
            var target = InitializeTarget(contextMock.Object);

            target.Update(eventGroupModel);
        }

        [TestMethod]
        public void Update_ValidIdAndUserId_ContextSaveIsCalled()
        {
            var eventGroupModel = new EventGroupModel { Id = 1 };
            var eventGroup = new EventGroup { Id = eventGroupModel.Id.Value, UserId = User.Id };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroupModel.Id))).Returns(eventGroup).Verifiable();
            contextMock.Setup(x => x.SaveChanges()).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Update(eventGroupModel);

            contextMock.Verify();
        }
        #endregion
    }
}
