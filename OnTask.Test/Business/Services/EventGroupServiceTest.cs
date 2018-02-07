using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnTask.Business.Services;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Contexts.Interfaces;
using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventGroupServiceTest
    {
        #region Constants
        private static User User = new User
        {
            Id = "foobar"
        };
        #endregion

        #region Initialization
        private IEventGroupService InitializeTarget(IOnTaskDbContext context)
        {
            IEventGroupService target = new EventGroupService(context);
            target.AddApplicationUser(User);
            return target;
        }
        #endregion

        #region Tests
        [TestMethod]
        public void Delete_InvalidId_DoesNotDeleteEventGroup()
        {
            var eventGroup = new EventGroup
            {
                Id = 1,
                UserId = User.Id
            };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroup.Id))).Returns<EventGroup>(null).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(eventGroup.Id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEventGroup(It.IsAny<EventGroup>()), Times.Never());
        }

        [TestMethod]
        public void Delete_InvalidUserId_DoesNotDeleteEventGroup()
        {
            var eventGroup = new EventGroup
            {
                Id = 1,
                UserId = "barbaz"
            };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroup.Id))).Returns(eventGroup).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(eventGroup.Id);

            contextMock.Verify();
            contextMock.Verify(x => x.DeleteEventGroup(It.IsAny<EventGroup>()), Times.Never());
        }

        [TestMethod]
        public void Delete_ValidIdAndUser_DeletesEventGroup()
        {
            var eventGroup = new EventGroup
            {
                Id = 1,
                UserId = User.Id
            };
            var contextMock = new Mock<IOnTaskDbContext>();
            contextMock.Setup(x => x.GetEventGroupByIdTracked(It.Is<int>(y => y == eventGroup.Id))).Returns(eventGroup).Verifiable();
            contextMock.Setup(x => x.DeleteEventGroup(It.Is<EventGroup>(y => y == eventGroup))).Verifiable();
            var target = InitializeTarget(contextMock.Object);

            target.Delete(eventGroup.Id);

            contextMock.Verify();
        }
        #endregion
    }
}
