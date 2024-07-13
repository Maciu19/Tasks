using Application.Common.JWT;
using Application.Contracts;
using Application.Errors;
using Application.Errors.Common;

using Domain.Entities;

using FluentValidation;

using Infrastructure.Repositories;

using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserAddRequest> _userAddRequestValidator;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository repository, 
        IValidator<UserAddRequest> userAddRequestValidator,
        IJwtProvider jwtProvider)
    {
        _repository = repository;
        _userAddRequestValidator = userAddRequestValidator;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<User?> GetByEmailAsync(string email) 
        => await _repository.GetByEmailAsync(email);

    public async Task<User?> GetByIdAsync(Guid id)
        => await _repository.GetByIdAsync(id);

    public async Task<User?> GetByUsernameAsync(string username)
        => await _repository.GetByUsernameAsync(username);

    public async Task<User?> GetByDisplayNameAsync(string displayName)
        => await _repository.GetByDisplayNameAsync(displayName);

    public async Task<User> CreateAsync(UserAddRequest request)
    {
        await _userAddRequestValidator.ValidateAndThrowAsync(request);

        if (await _repository.GetByUsernameAsync(request.Username) is not null)
            throw new CustomException(UserErrors.UsernameConflict(request.Username));

        if (await _repository.GetByEmailAsync(request.Email) is not null)
            throw new CustomException(UserErrors.EmailConflict(request.Email));

        if (await _repository.GetByDisplayNameAsync(request.DisplayName) is not null)
            throw new CustomException(UserErrors.DisplayNameConflict(request.DisplayName));

        User user = new(
            request.Username,
            request.Email,
            request.Password,
            request.DisplayName);

        string hashPassword = _passwordHasher.HashPassword(user, request.Password);

        user.Password = hashPassword;

        await _repository.AddAsync(user);

        return user;
    }

    public async Task UpdateAsync(UserUpdateRequest request)
    {
        var user = await _repository.GetByIdAsync(request.Id) ??
            throw new CustomException(UserErrors.NotFound($"User with id {request.Id} not found"));

        if (!user.DisplayName.Equals(request.NewDisplayName))
        {
            if (await _repository.GetByDisplayNameAsync(request.NewDisplayName) is not null)
                throw new CustomException(UserErrors.DisplayNameConflict(request.NewDisplayName));

            user.DisplayName = request.NewDisplayName;
        }

        string hashNewPassword = _passwordHasher.HashPassword(user, request.NewPassword);

        user.Password = hashNewPassword;

        await _repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id) ??
            throw new CustomException(UserErrors.NotFound($"User with id {id} not found"));

        user.Deleted = true;

        await _repository.DeleteAsync(user);
    }
}
