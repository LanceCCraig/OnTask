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
            RuleFor(x => x.Mode)
                .NotNull().WithMessage("A mode must be specified")
                .NotEmpty().WithMessage("A mode must be specified")
                .Must(BeAValidMode).WithMessage("Multiple event types can only be deleted by specifying a valid mode.");
            When(x => x.Mode == Constants.ModeByGroup, () =>
            {
                RuleFor(x => x.EventGroupId).NotNull().WithMessage("An event group must be specified.");
                RuleFor(x => x.EventParentId).Null().WithMessage("Only an event group may be specified.");
            });
            When(x => x.Mode == Constants.ModeByParent, () =>
            {
                RuleFor(x => x.EventGroupId).Null().WithMessage("Only an event parent may be specified.");
                RuleFor(x => x.EventParentId).NotNull().WithMessage("An event parent must be specified.");
            });
        }

        private static bool BeAValidMode(string mode) =>
            mode == Constants.ModeByGroup ||
            mode == Constants.ModeByParent;
    }
}
