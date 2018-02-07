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
    public class ResetPasswordModelValidatorTest
    {
        #region Constants
        private const string ExtraLongPassword = "Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1Foo1";
        private const string ValidPassword = "Foo1Bar2";
        #endregion

        #region Fields
        private readonly IValidator<ResetPasswordModel> target = new ResetPasswordModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("foo1bar2", DisplayName = "Unmatching")]
        public void Validate_InvalidConfirmPassword(string confirmPassword)
        {
            var model = new ResetPasswordModel
            {
                Password = ValidPassword,
                ConfirmPassword = confirmPassword
            };

            target.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, model);
        }

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
        [DataRow("Foo1", DisplayName = "Below Minimum Length")]
        [DataRow(ExtraLongPassword, DisplayName = "Above Maximum Length")]
        [DataRow("FooBarBaz", DisplayName = "Missing A Digit")]
        [DataRow("foo1bar2", DisplayName = "Missing An Uppercase Letter")]
        public void Validate_InvalidPassword(string password)
        {
            target.ShouldHaveValidationErrorFor(x => x.Password, password);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        public void Validate_InvalidToken(string token)
        {
            target.ShouldHaveValidationErrorFor(x => x.Token, token);
        }

        [TestMethod]
        public void Validate_ValidModel()
        {
            var model = new ResetPasswordModel
            {
                Email = "foo@bar.baz",
                Password = ValidPassword,
                ConfirmPassword = ValidPassword,
                Token = "foo"
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Email, model);
            target.ShouldNotHaveValidationErrorFor(x => x.Password, model);
            target.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, model);
            target.ShouldNotHaveValidationErrorFor(x => x.Token, model);
        }
        #endregion
    }
}
