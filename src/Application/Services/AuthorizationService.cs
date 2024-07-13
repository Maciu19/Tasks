using Application.Common.JWT;
using Application.Contracts;
using Application.Errors;
using Application.Errors.Common;

using Domain.Entities;

using FluentValidation;

using Infrastructure.Repositories;

using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserLoginRequest> _userLoginRequestValidator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthorizationService(
        IUserRepository repository,
        IJwtProvider jwtProvider,
        IValidator<UserLoginRequest> userLoginRequestValidator)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
        _userLoginRequestValidator = userLoginRequestValidator;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<string> Login(UserLoginRequest request)
    {
        await _userLoginRequestValidator.ValidateAndThrowAsync(request);

        User? user;

        if (!string.IsNullOrEmpty(request.Email))
        {
            user = await _repository.GetByEmailAsync(request.Email) ??
                throw new CustomException(UserErrors.InvalidCredentials);
        }
        else if (!string.IsNullOrEmpty(request.Username))
        {
            user = await _repository.GetByUsernameAsync(request.Username) ??
                throw new CustomException(UserErrors.InvalidCredentials);
        }
        else
            throw new CustomException(UserErrors.InvalidCredentials);

        var success = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        return success != PasswordVerificationResult.Success
            ? throw new CustomException(UserErrors.InvalidCredentials)
            : _jwtProvider.Generate(user);
    }
}
