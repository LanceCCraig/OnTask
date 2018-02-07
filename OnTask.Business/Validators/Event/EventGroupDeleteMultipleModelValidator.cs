using FluentValidation;
using OnTask.Business.Models.Event;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for a <see cref="EventGroupDeleteMultipleModel"/> class.
    /// </summary>
    public class EventGroupDeleteMultipleModelValidator : AbstractValidator<EventGroupDeleteMultipleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventGroupDeleteMultipleModelValidator"/> class.
        /// </summary>
        public EventGroupDeleteMultipleModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.EventParentId).NotEmpty().WithMessage("An event parent must be specified.");
        }
    }
}
