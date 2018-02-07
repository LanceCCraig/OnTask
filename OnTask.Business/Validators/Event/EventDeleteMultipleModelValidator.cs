using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;
using System;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for a <see cref="EventDeleteMultipleModel"/> class.
    /// </summary>
    public class EventDeleteMultipleModelValidator : AbstractValidator<EventDeleteMultipleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDeleteMultipleModelValidator"/> class.
        /// </summary>
        public EventDeleteMultipleModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Mode)
                .NotNull().WithMessage("A mode must be specified")
                .NotEmpty().WithMessage("A mode must be specified")
                .Must(BeAValidMode).WithMessage("Multiple events can only be deleted by specifying a valid mode.");
            When(x => string.Equals(x.Mode, Constants.ModeByType, StringComparison.CurrentCultureIgnoreCase), () =>
            {
                RuleFor(x => x.EventTypeId).NotNull().WithMessage("An event type must be specified.");
                RuleFor(x => x.EventGroupId).Null().WithMessage("Only an event type may be specified.");
                RuleFor(x => x.EventParentId).Null().WithMessage("Only an event type may be specified.");
            });
            When(x => string.Equals(x.Mode, Constants.ModeByGroup, StringComparison.CurrentCultureIgnoreCase), () =>
            {
                RuleFor(x => x.EventTypeId).Null().WithMessage("Only an event group may be specified.");
                RuleFor(x => x.EventGroupId).NotNull().WithMessage("An event group must be specified.");
                RuleFor(x => x.EventParentId).Null().WithMessage("Only an event group may be specified.");
            });
            When(x => string.Equals(x.Mode, Constants.ModeByParent, StringComparison.CurrentCultureIgnoreCase), () =>
            {
                RuleFor(x => x.EventTypeId).Null().WithMessage("Only an event parent may be specified.");
                RuleFor(x => x.EventGroupId).Null().WithMessage("Only an event parent may be specified.");
                RuleFor(x => x.EventParentId).NotNull().WithMessage("An event parent must be specified.");
            });
        }

        private static bool BeAValidMode(string mode) =>
            string.Equals(mode, Constants.ModeByType, StringComparison.CurrentCultureIgnoreCase) ||
            string.Equals(mode, Constants.ModeByGroup, StringComparison.CurrentCultureIgnoreCase) ||
            string.Equals(mode, Constants.ModeByParent, StringComparison.CurrentCultureIgnoreCase);
    }
}
