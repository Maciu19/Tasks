using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Common.JWT;
using Application.Errors;
using Application.Errors.Common;

using Domain.Entities;

using Infrastructure.Repositories;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _options;
    private readonly ITokenRepository _tokenRepository;

    public TokenService(
        IOptions<JwtOptions> options,
        ITokenRepository tokenRepository)
    {
        _options = options.Value;
        _tokenRepository = tokenRepository;

    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(3),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public async Task<RefreshToken> GenerateRefreshToken(User user)
    {
        RefreshToken refreshToken = new(
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(30),
            user.Id);

        await _tokenRepository.UpsertRefreshTokenAsync(refreshToken);

        return refreshToken;
    }

    public Guid GetUserIdFromExpiredAccessToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new CustomException(AuthorizationErrors.InvalidAccessToken);
        }

        string? id = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        return id is null
            ? throw new CustomException(AuthorizationErrors.InvalidAccessToken)
            : Guid.Parse(id);
    }

    public async Task ValidateRefreshToken(Guid token, User user)
    {
        RefreshToken refreshToken = await _tokenRepository.GetRefreshTokenAsync(token)
            ?? throw new CustomException(AuthorizationErrors.InvalidRefreshToken);

        if (refreshToken.UserId != user.Id)
            throw new CustomException(AuthorizationErrors.InvalidRefreshToken);

        if (refreshToken.ExpirationDate < DateTime.UtcNow)
            throw new CustomException(AuthorizationErrors.ExpiredRefreshToken);
    }

    public Task InvalidateRefreshToken(Guid token, Guid userId)
    {
        return _tokenRepository.DeleteRefreshTokenAsync(token, userId);
    }
}
