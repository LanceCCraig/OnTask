using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Account;
using OnTask.Business.Validators.Account;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Validators.Account
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LoginModelValidatorTest
    {
        #region Fields
        private readonly IValidator<LoginModel> target = new LoginModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        [DataRow("foo", DisplayName = "Invalid Email Address")]
        public void Validate_InvalidEmail(string email)
        {
            target.ShouldHaveValidationErrorFor(x => x.Email, email);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        public void Validate_InvalidPassword(string password)
        {
            target.ShouldHaveValidationErrorFor(x => x.Password, password);
        }

        [TestMethod]
        public void Validate_ValidModel()
        {
            var model = new LoginModel
            {
                Email = "foo@bar.baz",
                Password = "foo"
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Email, model);
            target.ShouldNotHaveValidationErrorFor(x => x.Password, model);
        }
        #endregion
    }
}
