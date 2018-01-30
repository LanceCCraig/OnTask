using FluentValidation;
using OnTask.Business.Models.Account;
using OnTask.Common;

namespace OnTask.Business.Validators.Account
{
    /// <summary>
    /// Provides validation for a <see cref="ResetPasswordModel"/> class.
    /// </summary>
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordModelValidator"/> class.
        /// </summary>
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("An email is required.")
                .NotEmpty().WithMessage("An email is required.")
                .DependentRules(d =>
                {
                    d.RuleFor(y => y.Email).EmailAddress().WithMessage("The email is not valid.");
                });
            RuleFor(x => x.Password)
                .NotNull().WithMessage("A password is required.")
                .MinimumLength(Constants.MinimumPasswordLength).WithMessage($"The password must be at least {Constants.MinimumPasswordLength} characters.")
                .MaximumLength(Constants.MaximumPasswordLength).WithMessage($"The password must be no more than {Constants.MaximumPasswordLength} characters.")
                .DependentRules(d =>
                {
                    RuleFor(x => x.ConfirmPassword)
                        .NotNull().WithMessage("A confirmation password is required.")
                        .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
                });
        }
    }
}
