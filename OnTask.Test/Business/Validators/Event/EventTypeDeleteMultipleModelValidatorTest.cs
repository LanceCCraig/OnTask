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
    public class EventTypeDeleteMultipleModelValidatorTest
    {
        #region Fields
        private readonly IValidator<EventTypeDeleteMultipleModel> target = new EventTypeDeleteMultipleModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByGroup, null, DisplayName = "No Value For Group Mode")]
        [DataRow(Constants.ModeByParent, 1, DisplayName = "Value For Parent Mode")]
        public void Validate_InvalidEventGroupId(string mode, int? eventGroupId)
        {
            var model = new EventTypeDeleteMultipleModel
            {
                Mode = mode,
                EventGroupId = eventGroupId
            };

            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldHaveValidationErrorFor(x => x.EventGroupId, model);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByGroup, 1, DisplayName = "Value For Group Mode")]
        [DataRow(Constants.ModeByParent, null, DisplayName = "No Value For Parent Mode")]
        public void Validate_InvalidEventParentId(string mode, int? eventParentId)
        {
            var model = new EventTypeDeleteMultipleModel
            {
                Mode = mode,
                EventParentId = eventParentId
            };

            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldHaveValidationErrorFor(x => x.EventParentId, model);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        [DataRow("foo", DisplayName = "Unmatching")]
        public void Validate_InvalidMode(string mode)
        {
            target.ShouldHaveValidationErrorFor(x => x.Mode, mode);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByGroup, 1, null, DisplayName = "Group Mode")]
        [DataRow(Constants.ModeByParent, null, 1, DisplayName = "Parent Mode")]
        public void Validate_ValidModel(string mode, int? eventGroupId, int? eventParentId)
        {
            var model = new EventTypeDeleteMultipleModel
            {
                Mode = mode,
                EventGroupId = eventGroupId,
                EventParentId = eventParentId
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventGroupId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventParentId, model);
        }
        #endregion
    }
}
