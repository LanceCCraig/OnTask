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
    public class EventDeleteMultipleModelValidatorTest
    {
        #region Fields
        private readonly IValidator<EventDeleteMultipleModel> target = new EventDeleteMultipleModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByType, 1, DisplayName = "Value For Type Mode")]
        [DataRow(Constants.ModeByGroup, null, DisplayName = "No Value For Group Mode")]
        [DataRow(Constants.ModeByParent, 1, DisplayName = "Value For Parent Mode")]
        public void Validate_InvalidEventGroupId(string mode, int? eventGroupId)
        {
            var model = new EventDeleteMultipleModel
            {
                Mode = mode,
                EventGroupId = eventGroupId
            };

            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldHaveValidationErrorFor(x => x.EventGroupId, model);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByType, 1, DisplayName = "Value For Type Mode")]
        [DataRow(Constants.ModeByGroup, 1, DisplayName = "Value For Group Mode")]
        [DataRow(Constants.ModeByParent, null, DisplayName = "No Value For Parent Mode")]
        public void Validate_InvalidEventParentId(string mode, int? eventParentId)
        {
            var model = new EventDeleteMultipleModel
            {
                Mode = mode,
                EventParentId = eventParentId
            };

            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldHaveValidationErrorFor(x => x.EventParentId, model);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(Constants.ModeByType, null, DisplayName = "No Value For Type Mode")]
        [DataRow(Constants.ModeByGroup, 1, DisplayName = "Value For Group Mode")]
        [DataRow(Constants.ModeByParent, 1, DisplayName = "Value For Parent Mode")]
        public void Validate_InvalidEventTypeId(string mode, int? eventTypeId)
        {
            var model = new EventDeleteMultipleModel
            {
                Mode = mode,
                EventTypeId = eventTypeId
            };

            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldHaveValidationErrorFor(x => x.EventTypeId, model);
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
        [DataRow(Constants.ModeByType, 1, null, null, DisplayName = "Type Mode")]
        [DataRow(Constants.ModeByGroup, null, 1, null, DisplayName = "Group Mode")]
        [DataRow(Constants.ModeByParent, null, null, 1, DisplayName = "Parent Mode")]
        public void Validate_ValidModel(string mode, int? eventTypeId, int? eventGroupId, int? eventParentId)
        {
            var model = new EventDeleteMultipleModel
            {
                Mode = mode,
                EventTypeId = eventTypeId,
                EventGroupId = eventGroupId,
                EventParentId = eventParentId
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Mode, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventTypeId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventGroupId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventParentId, model);
        }
        #endregion
    }
}
