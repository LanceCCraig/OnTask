using FluentValidation;
using OnTask.Business.Models.Account;

namespace OnTask.Business.Validators.Account
{
    /// <summary>
    /// Provides validator for an <see cref="ExternalLoginModel"/> class.
    /// </summary>
    public class ExternalLoginModelValidator : AbstractValidator<ExternalLoginModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLoginModelValidator"/> class.
        /// </summary>
        public ExternalLoginModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Email)
                .NotNull().WithMessage("An email is required.")
                .NotEmpty().WithMessage("An email is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email).EmailAddress().WithMessage("The email is not valid");
                });
        }
    }
}
