using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for an <see cref="EventTypeModel"/> class.
    /// </summary>
    public class EventTypeModelValidator : AbstractValidator<EventTypeModel>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeModelValidator"/> class.
        /// </summary>
        public EventTypeModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleSet(Constants.RuleSetNameForInsert, () =>
            {
                RuleFor(x => x.Id).Null().WithMessage("The event type identifier cannot be manually set.");
                ExecuteCommonRules();
            });
            RuleSet(Constants.RuleSetNameForUpdate, () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("An event type must be specified.");
                ExecuteCommonRules();
            });
        }
        #endregion

        #region Private Helpers
        private void ExecuteCommonRules()
        {
            RuleFor(x => x.EventGroupId).NotEmpty().WithMessage("An event group is required.");
            RuleFor(x => x.EventParentId).NotEmpty().WithMessage("An event parent is required.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required")
                .NotEmpty().WithMessage("A name is required");
        } 
        #endregion
    }
}
