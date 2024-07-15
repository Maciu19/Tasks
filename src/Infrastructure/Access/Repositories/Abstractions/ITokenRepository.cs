using Domain.Access;

namespace Infrastructure.Access.Repositories.Abstractions;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(Guid token);
    Task UpsertRefreshTokenAsync(RefreshToken token);
    Task DeleteRefreshTokenAsync(Guid token, Guid userId);
}
