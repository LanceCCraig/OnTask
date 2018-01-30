using FluentValidation;
using OnTask.Business.Models.Account;

namespace OnTask.Business.Validators.Account
{
    /// <summary>
    /// Provides validation for a <see cref="LoginModel"/> class.
    /// </summary>
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModelValidator"/> class.
        /// </summary>
        public LoginModelValidator()
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
                .NotEmpty().WithMessage("A password is required.");
        }
    }
}
