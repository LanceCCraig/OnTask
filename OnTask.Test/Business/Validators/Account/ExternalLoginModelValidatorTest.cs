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
    public class ExternalLoginModelValidatorTest
    {
        #region Fields
        private readonly IValidator<ExternalLoginModel> target = new ExternalLoginModelValidator();
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
        public void Validate_ValidModel()
        {
            var model = new ExternalLoginModel
            {
                Email = "foo@bar.baz"
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Email, model);
        }
        #endregion
    }
}
