using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Event;
using OnTask.Business.Validators.Event;
using OnTask.Common;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Validators.Event
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventParentModelValidatorTest
    {
        #region Fields
        private readonly IValidator<EventParentModel> target = new EventParentModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, Constants.RuleSetNameForInsert, DisplayName = "Has Value For Insert")]
        [DataRow(null, Constants.RuleSetNameForUpdate, DisplayName = "Has No Value For Update")]
        public void Validate_InvalidId(int? id, string ruleSet)
        {
            target.ShouldHaveValidationErrorFor(x => x.Id, id, ruleSet);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        public void Validate_InvalidName(string name)
        {
            target.ShouldHaveValidationErrorFor(x => x.Name, name, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.Name, name, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, "foo", Constants.RuleSetNameForInsert, DisplayName = "Insert")]
        [DataRow(1, "foo", Constants.RuleSetNameForUpdate, DisplayName = "Update")]
        public void Validate_ValidModel(int? id, string name, string ruleSet)
        {
            var model = new EventParentModel
            {
                Id = id,
                Name = name
            };

            var result = target.Validate(model, ruleSet);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Id, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.Name, model, ruleSet);
        }
        #endregion
    }
}
