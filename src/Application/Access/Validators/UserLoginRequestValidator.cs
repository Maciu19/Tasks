using Application.Access.Contracts;

using FluentValidation;

namespace Application.Access.Validators;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().When(x => string.IsNullOrEmpty(x.Username));
        RuleFor(x => x.Username).NotEmpty().NotNull().When(x => string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Password).NotEmpty();
    }
}
