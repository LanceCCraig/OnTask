using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Event;
using OnTask.Business.Validators.Event;
using OnTask.Common;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Validators.Event
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EventModelValidatorTest
    {
        #region Fields
        private readonly IValidator<EventModel> target = new EventModelValidator();
        #endregion

        #region Test
        [TestMethod]
        public void Validate_InvalidEventGroupId()
        {
            var eventGroupId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventGroupId, eventGroupId, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.EventGroupId, eventGroupId, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        public void Validate_InvalidEventParentId()
        {
            var eventParentId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventParentId, eventParentId, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.EventParentId, eventParentId, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        public void Validate_InvalidEventTypeId()
        {
            var eventTypeId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventTypeId, eventTypeId, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.EventTypeId, eventTypeId, Constants.RuleSetNameForUpdate);
        }

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
        public void Validate_InvalidStartDate()
        {
            var startDate = default(DateTime);

            target.ShouldHaveValidationErrorFor(x => x.StartDate, startDate, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.StartDate, startDate, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        public void Validate_InvalidEndDate()
        {
            var endDate = default(DateTime);

            target.ShouldHaveValidationErrorFor(x => x.EndDate, endDate, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.EndDate, endDate, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        public void Validate_InvalidStartTimeAndEndTime()
        {
            var model = new EventModel();

            target.ShouldHaveValidationErrorFor(x => x.StartTime, model, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.StartTime, model, Constants.RuleSetNameForUpdate);
            target.ShouldHaveValidationErrorFor(x => x.EndTime, model, Constants.RuleSetNameForInsert);
            target.ShouldHaveValidationErrorFor(x => x.EndTime, model, Constants.RuleSetNameForUpdate);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, 1, 1, 1, "foo", "2018-01-01", Constants.RuleSetNameForInsert, DisplayName = "Insert")]
        [DataRow(1, 1, 1, 1, "foo", "2018-01-01", Constants.RuleSetNameForUpdate, DisplayName = "Update")]
        public void Validate_ValidModel(int? id, int eventGroupId, int eventParentId, int eventTypeId, string name, string startDateText, string ruleSet)
        {
            var model = new EventModel
            {
                Id = id,
                EventGroupId = eventGroupId,
                EventParentId = eventParentId,
                EventTypeId = eventTypeId,
                Name = name,
                StartDate = Convert.ToDateTime(startDateText)
            };

            var result = target.Validate(model, ruleSet);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.Id, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.EventGroupId, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.EventParentId, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.EventTypeId, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.Name, model, ruleSet);
            target.ShouldNotHaveValidationErrorFor(x => x.StartDate, model, ruleSet);
        }
        #endregion
    }
}
