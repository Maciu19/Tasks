using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(Guid token);
    Task UpsertRefreshTokenAsync(RefreshToken token);
    Task DeleteRefreshTokenAsync(Guid token, Guid userId);
}
