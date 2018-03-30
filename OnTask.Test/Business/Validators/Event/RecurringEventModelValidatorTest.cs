using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTask.Business.Models.Event;
using OnTask.Business.Validators.Event;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OnTask.Test.Business.Validators.Event
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RecurringEventModelValidatorTest
    {
        #region Fields
        private readonly IValidator<RecurringEventModel> target = new RecurringEventModelValidator();
        #endregion

        #region Tests
        [TestMethod]
        public void Validate_InvalidEventParentId()
        {
            var eventParentId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventParentId, eventParentId);
        }

        [TestMethod]
        public void Validate_InvalidEventGroupId()
        {
            var eventGroupId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventGroupId, eventGroupId);
        }

        [TestMethod]
        public void Validate_InvalidEventTypeId()
        {
            var eventTypeId = default(int);

            target.ShouldHaveValidationErrorFor(x => x.EventTypeId, eventTypeId);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow("", DisplayName = "Empty")]
        public void Validate_InvalidName(string name)
        {
            target.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("-01:00:00", DisplayName = "Negative")]
        [DataRow("24:00:00", DisplayName = "Midnight")]
        [DataRow("48:00:00", DisplayName = "After Midnight")]
        public void Validate_InvalidStartTime(string startTimeText)
        {
            var startTime = TimeSpan.Parse(startTimeText);

            target.ShouldHaveValidationErrorFor(x => x.StartTime, startTime);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("-01:00:00", DisplayName = "Negative")]
        [DataRow("24:00:00", DisplayName = "Midnight")]
        [DataRow("48:00:00", DisplayName = "After Midnight")]
        public void Validate_InvalidEndTime(string endTimeText)
        {
            var endTime = TimeSpan.Parse(endTimeText);

            target.ShouldHaveValidationErrorFor(x => x.EndTime, endTime);
        }

        [TestMethod]
        public void Validate_InvalidDateRangeStart()
        {
            var dateRangeStart = default(DateTime);

            target.ShouldHaveValidationErrorFor(x => x.DateRangeStart, dateRangeStart);
        }

        [TestMethod]
        public void Validate_InvalidDateRangeEnd()
        {
            var dateRangeEnd = default(DateTime);

            target.ShouldHaveValidationErrorFor(x => x.DateRangeEnd, dateRangeEnd);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(null, DisplayName = "Null")]
        [DataRow(new string[] { }, DisplayName = "Empty")]
        [DataRow(new[] { "foo" }, DisplayName = "Invalid Value")]
        [DataRow(new[] { "Monday", "Tuesday", "foo", "bar" }, DisplayName = "Some Invalid Values")]
        [DataRow(new[] { "foo", "bar" }, DisplayName = "Invalid Values")]
        public void Validate_InvalidDaysOfWeek(string[] daysOfWeek)
        {
            target.ShouldHaveValidationErrorFor(x => x.DaysOfWeek, daysOfWeek);
        }

        [TestMethod]
        public void Validate_ValidModel()
        {
            var model = new RecurringEventModel
            {
                EventParentId = 1,
                EventGroupId = 1,
                EventTypeId = 1,
                Name = "foo",
                StartTime = new TimeSpan(12, 30, 0),
                EndTime = new TimeSpan(13, 50, 0),
                DateRangeStart = new DateTime(2018, 1, 8),
                DateRangeEnd = new DateTime(2018, 4, 20),
                DaysOfWeek = new[]
                {
                    "Tuesday",
                    "Thursday"
                }
            };

            var result = target.Validate(model);

            Assert.IsTrue(result.IsValid);
            target.ShouldNotHaveValidationErrorFor(x => x.EventParentId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventGroupId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EventTypeId, model);
            target.ShouldNotHaveValidationErrorFor(x => x.Name, model);
            target.ShouldNotHaveValidationErrorFor(x => x.StartTime, model);
            target.ShouldNotHaveValidationErrorFor(x => x.EndTime, model);
            target.ShouldNotHaveValidationErrorFor(x => x.DateRangeStart, model);
            target.ShouldNotHaveValidationErrorFor(x => x.DateRangeEnd, model);
            target.ShouldNotHaveValidationErrorFor(x => x.DaysOfWeek, model);
        }
        #endregion
    }
}
