using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;
using System;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for an <see cref="EventModel"/> class.
    /// </summary>
    public class EventModelValidator : AbstractValidator<EventModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventModelValidator"/> class.
        /// </summary>
        public EventModelValidator()
        {
            RuleSet(Constants.RuleSetNameForInsert, () =>
            {
                ExecuteCommonRules();
            });
            RuleSet(Constants.RuleSetNameForUpdate, () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("An event must be specified.");
                ExecuteCommonRules();
            });
        }

        private void ExecuteCommonRules()
        {
            RuleFor(x => x.EventGroupId).NotNull().WithMessage("An event group is required.");
            RuleFor(x => x.EventParentId).NotNull().WithMessage("An event parent is required.");
            RuleFor(x => x.EventTypeId).NotNull().WithMessage("An event type is required.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required.")
                .NotEmpty().WithMessage("A name is required.");
            RuleFor(x => x.StartDate)
                .NotEqual(default(DateTime)).WithMessage("A start date is required.");
        }
    }
}
