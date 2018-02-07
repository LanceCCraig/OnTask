using FluentValidation;
using OnTask.Business.Models.Event;
using OnTask.Common;

namespace OnTask.Business.Validators.Event
{
    /// <summary>
    /// Provides validation for an <see cref="EventParentModel"/> clas.
    /// </summary>
    public class EventParentModelValidator : AbstractValidator<EventParentModel>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParentModelValidator"/> class.
        /// </summary>
        public EventParentModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleSet(Constants.RuleSetNameForInsert, () =>
            {
                RuleFor(x => x.Id).Null().WithMessage("The event parent identifier cannot be manually set.");
                ExecuteCommonRules();
            });
            RuleSet(Constants.RuleSetNameForUpdate, () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("An event parent must be specified.");
                ExecuteCommonRules();
            });
        }
        #endregion

        #region Private Helpers
        private void ExecuteCommonRules()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("A name is required.")
                .NotEmpty().WithMessage("A name is required.");
        } 
        #endregion
    }
}
