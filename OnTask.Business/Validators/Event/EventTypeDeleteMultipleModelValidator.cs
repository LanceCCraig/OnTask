using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for a <see cref="EventTypeDeleteMultipleModel"/> class.
    /// </summary>
    public class EventTypeDeleteMultipleModelValidator : AbstractValidator<EventTypeDeleteMultipleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeDeleteMultipleModelValidator"/> class.
        /// </summary>
        public EventTypeDeleteMultipleModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Parent)
                .NotNull().WithMessage("A parent must be specified")
                .NotEmpty().WithMessage("A parent must be specified")
                .Must(BeAValidParent).WithMessage("Multiple event types can only be deleted by specifying a valid parent.");
            When(x => x.Parent == Constants.RuleSetNameForMultipleModelsByGroup, () =>
            {
                RuleFor(x => x.EventGroupId).NotNull().WithMessage("An event group must be specified.");
                RuleFor(x => x.EventParentId).Null().WithMessage("Only an event group may be specified.");
            });
            When(x => x.Parent == Constants.RuleSetNameForMultipleModelsByParent, () =>
            {
                RuleFor(x => x.EventGroupId).Null().WithMessage("Only an event parent may be specified.");
                RuleFor(x => x.EventParentId).NotNull().WithMessage("An event parent must be specified.");
            });
        }

        private static bool BeAValidParent(string parent) =>
            parent == Constants.RuleSetNameForMultipleModelsByGroup ||
            parent == Constants.RuleSetNameForMultipleModelsByParent;
    }
}
