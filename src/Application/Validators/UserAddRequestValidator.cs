using Application.Contracts;

using FluentValidation;

namespace Domain.Validators;

public class UserAddRequestValidator : AbstractValidator<UserAddRequest>
{
    public UserAddRequestValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(32).WithMessage("Your password length must not exceed 32.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\@\#\$\%\^\&\*\?\.\;\:]+").WithMessage("Your password must contain at least one (!@#$%^&*?.;:).");
        RuleFor(x => x.DisplayName).NotNull().NotEmpty();
    }
}