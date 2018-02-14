using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Services;
using OnTask.Business.Services.Interfaces;
using OnTask.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseServiceTest
    {
        #region Fields
        private readonly IBaseService target = new BaseService();
        #endregion

        #region Tests
        [TestMethod]
        public void AddApplicationUser_ValueIsSet()
        {
            var user = new User
            {
                UserName = "foo"
            };
            var expected = user;

            target.AddApplicationUser(user);
            var actual = target.ApplicationUser;

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
