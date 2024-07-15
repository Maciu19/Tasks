using Application.Access.Contracts;

using Domain.Access;

namespace Application.Access.Services.Abstractions;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByDisplayNameAsync(string displayName);
    Task<User> CreateAsync(UserAddRequest request);
    Task UpdateAsync(UserUpdateRequest request);
    Task DeleteAsync(Guid id);
}
