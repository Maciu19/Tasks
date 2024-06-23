using Application.Contracts;

using Domain.Entities;

namespace Application.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByDisplayNameAsync(string displayName);
    Task<User> CreateAsync(UserAddRequest request);
    Task DeleteAsync(Guid id);
}
