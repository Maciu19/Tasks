using Domain.Entities;

namespace Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    Guid GetUserIdFromExpiredAccessToken(string token);
    Task<RefreshToken> GenerateRefreshToken(User user);
    Task ValidateRefreshToken(Guid token, User user);
    Task InvalidateRefreshToken(Guid token, Guid userId);
}
