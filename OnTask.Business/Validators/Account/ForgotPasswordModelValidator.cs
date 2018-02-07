using FluentValidation;
using OnTask.Business.Models.Account;

namespace OnTask.Business.Validators.Account
{
    /// <summary>
    /// Provides validation for a <see cref="ForgotPasswordModel"/> class.
    /// </summary>
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordModelValidator"/> class.
        /// </summary>
        public ForgotPasswordModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Email)
                .NotNull().WithMessage("An email is required.")
                .NotEmpty().WithMessage("An email is required.")
                .DependentRules(() =>
                {
                    RuleFor(y => y.Email).EmailAddress().WithMessage("The email is not valid.");
                });
        }
    }
}
