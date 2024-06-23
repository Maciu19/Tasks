using Application.Contracts;
using Application.Errors;
using Application.Errors.Common;

using Domain.Entities;

using FluentValidation;

using Infrastructure.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserAddRequest> _userAddRequestValidator;

    public UserService(IUserRepository repository, IValidator<UserAddRequest> userAddRequestValidator)
    {
        _repository = repository;
        _userAddRequestValidator = userAddRequestValidator;
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

        // TODO: Hash the Password

        User user = new(
            request.Username,
            request.Email,
            request.Password,
            request.DisplayName);

        await _repository.AddAsync(user);

        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id) ??
            throw new CustomException(UserErrors.NotFound(id));

        user.Deleted = true;

        await _repository.DeleteAsync(user);
    }
}
