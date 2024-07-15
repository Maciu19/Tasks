using Dapper;

using Domain.Access;

using Infrastructure.Access.Repositories.Abstractions;
using Infrastructure.Common.DatabaseProvider;

using Npgsql;

namespace Infrastructure.Access.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly NpgsqlConnection _connection;

    public TokenRepository(IDatabaseProvider databaseProvider)
    {
        _connection = new NpgsqlConnection(databaseProvider.GetDefaultConnectionString());
    }

    public async Task UpsertRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _connection.ExecuteAsync(
            sql:
            """
                INSERT INTO user_refresh_token (token, user_id, expiration_date)
                VALUES (@Token, @UserId, @ExpirationDate)
                ON CONFLICT (user_id)
                DO UPDATE SET
                    token = @Token,
                    expiration_date = @ExpirationDate
            """,
            param: refreshToken
        );
    }

    public Task<RefreshToken?> GetRefreshTokenAsync(Guid token)
    {
        return _connection.QueryFirstOrDefaultAsync<RefreshToken>(
            sql:
            """
                SELECT token, user_id, expiration_date
                FROM user_refresh_token
                WHERE token = @Token
            """,
            param: new { Token = token }
        );
    }

    public Task DeleteRefreshTokenAsync(Guid token, Guid userId)
    {
        return _connection.ExecuteAsync(
            sql:
            """
                DELETE FROM user_refresh_token
                WHERE token = @Token AND user_id = @UserId
            """,
            param: new { Token = token, UserId = userId }
        );
    }
}
