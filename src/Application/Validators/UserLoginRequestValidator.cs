using Application.Contracts;

using FluentValidation;

namespace Application.Validators;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().When(x => string.IsNullOrEmpty(x.Username));
        RuleFor(x => x.Username).NotEmpty().NotNull().When(x => string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Password).NotEmpty();
    }
}
