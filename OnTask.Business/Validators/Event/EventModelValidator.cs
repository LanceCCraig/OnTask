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
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventModelValidator"/> class.
        /// </summary>
        public EventModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleSet(Constants.RuleSetNameForInsert, () =>
            {
                RuleFor(x => x.Id).Null().WithMessage("The event identifier cannot be manually set.");
                ExecuteCommonRules();
            });
            RuleSet(Constants.RuleSetNameForUpdate, () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("An event must be specified.");
                ExecuteCommonRules();
            });
        }
        #endregion

        #region Private Helpers
        private void ExecuteCommonRules()
        {
            RuleFor(x => x.EventGroupId).NotEmpty().WithMessage("An event group is required.");
            RuleFor(x => x.EventParentId).NotEmpty().WithMessage("An event parent is required.");
            RuleFor(x => x.EventTypeId).NotEmpty().WithMessage("An event type is required.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required.")
                .NotEmpty().WithMessage("A name is required.");
            RuleFor(x => x.StartDate)
                .NotEqual(default(DateTime)).WithMessage("A start date is required.");
        } 
        #endregion
    }
}
