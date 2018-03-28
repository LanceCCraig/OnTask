using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;
using System;
using System.Collections.Generic;
using static OnTask.Common.Enumerations;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for a <see cref="RecurringEventModel"/> class.
    /// </summary>
    public class RecurringEventModelValidator : AbstractValidator<RecurringEventModel>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringEventModel"/> class.
        /// </summary>
        public RecurringEventModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.EventParentId).NotEmpty().WithMessage("An event parent is required.");
            RuleFor(x => x.EventGroupId).NotEmpty().WithMessage("An event group is required.");
            RuleFor(x => x.EventTypeId).NotEmpty().WithMessage("An event type is required.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required.")
                .NotEmpty().WithMessage("A name is required.");
            RuleFor(x => x.StartTime)
                .GreaterThanOrEqualTo(Constants.MinimumTimeSpan).WithMessage("The start time cannot be negative.")
                .LessThan(Constants.MaximumTimeSpan).WithMessage("The start time must be before midnight.");
            When(x => x.EndTime.HasValue, () =>
            {
                RuleFor(x => x.EndTime)
                    .GreaterThanOrEqualTo(Constants.MinimumTimeSpan).WithMessage("The end time cannot be negative.")
                    .LessThan(Constants.MaximumTimeSpan).WithMessage("The end time must be before midnight.");
            });
            RuleFor(x => x.StartDate).NotEqual(default(DateTime)).WithMessage("A start date is required.");
            RuleFor(x => x.EndDate).NotEqual(default(DateTime)).WithMessage("An end date is required.");
            RuleFor(x => x.DaysOfWeek)
                .NotNull().WithMessage("At least one day of the week must be specified.")
                .NotEmpty().WithMessage("At least one day of the week must be specified.")
                .Must(HaveValidDaysOfWeekValues);
        }
        #endregion

        #region Private Helpers
        private static bool HaveValidDaysOfWeekValues(IEnumerable<string> daysOfWeek)
        {
            var isValid = true;
            foreach (var daysOfWeekText in daysOfWeek)
            {
                if (!Enum.TryParse(daysOfWeekText, out DaysOfWeek result) ||
                    result == DaysOfWeek.None)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        #endregion
    }
}
