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
    private readonly ITokenService _tokenService;

    public AuthorizationService(
        IUserRepository repository,
        ITokenService jwtProvider,
        IValidator<UserLoginRequest> userLoginRequestValidator)
    {
        _repository = repository;
        _tokenService = jwtProvider;
        _userLoginRequestValidator = userLoginRequestValidator;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest request)
    {
        await _userLoginRequestValidator.ValidateAndThrowAsync(request);

        User? user;

        if (!string.IsNullOrEmpty(request.Email))
        {
            user = await _repository.GetByEmailAsync(request.Email) ??
                throw new CustomException(AuthorizationErrors.InvalidCredentials);
        }
        else if (!string.IsNullOrEmpty(request.Username))
        {
            user = await _repository.GetByUsernameAsync(request.Username) ??
                throw new CustomException(AuthorizationErrors.InvalidCredentials);
        }
        else
            throw new CustomException(AuthorizationErrors.InvalidCredentials);

        var success = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        return success != PasswordVerificationResult.Success
            ? throw new CustomException(AuthorizationErrors.InvalidCredentials)
            : new UserLoginResponse(
                _tokenService.GenerateAccessToken(user),
                (await _tokenService.GenerateRefreshToken(user)).Token);
    }

    public async Task<UserLoginResponse> Refresh(RefreshRequest request)
    {
        Guid userId = _tokenService.GetUserIdFromExpiredAccessToken(request.AccessToken);

        User user = await _repository.GetByIdAsync(userId) ?? 
            throw new CustomException(AuthorizationErrors.InvalidCredentials);

        await _tokenService.ValidateRefreshToken(request.RefreshToken, user);

        var newAccessToken = _tokenService.GenerateAccessToken(user);

        return new UserLoginResponse(newAccessToken, request.RefreshToken);
    }

    public async Task Logout(UserLogoutRequest request)
    {
        Guid userId = _tokenService.GetUserIdFromExpiredAccessToken(request.AccessToken);

        await _tokenService.InvalidateRefreshToken(request.RefreshToken, userId);
    }
}
