using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for an <see cref="EventGroupModel"/> class.
    /// </summary>
    public class EventGroupModelValidator : AbstractValidator<EventGroupModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventGroupModelValidator"/> class.
        /// </summary>
        public EventGroupModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleSet(Constants.RuleSetNameForInsert, () =>
            {
                RuleFor(x => x.Id).Null().WithMessage("The event group identifier cannot be manually set.");
                ExecuteCommonRules();
            });
            RuleSet(Constants.RuleSetNameForUpdate, () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("An event group must be specified.");
                ExecuteCommonRules();
            });
        }

        private void ExecuteCommonRules()
        {
            RuleFor(x => x.EventParentId).NotEmpty().WithMessage("An event parent is required.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required.")
                .NotEmpty().WithMessage("A name is required.");
        }
    }
}
