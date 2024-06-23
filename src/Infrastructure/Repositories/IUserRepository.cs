using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByDisplayNameAsync(string displayName);
    Task<int> AddAsync(User user);
    Task<int> UpdateAsync(User user);
    Task<int> DeleteAsync(User user);
}
