using Dapper;

using Domain.Access;

using Infrastructure.Access.Queries;
using Infrastructure.Access.Repositories.Abstractions;
using Infrastructure.Common.DatabaseProvider;

using Npgsql;

namespace Infrastructure.Access.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NpgsqlConnection _connection;

    public UserRepository(IDatabaseProvider databaseProvider)
    {
        _connection = new NpgsqlConnection(databaseProvider.GetDefaultConnectionString());
    }

    public Task<User?> GetByEmailAsync(string email)
        => _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByEmail, new { Email = email });

    public Task<User?> GetByIdAsync(Guid id)
        => _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectById, new { Id = id });

    public Task<User?> GetByUsernameAsync(string username)
        => _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByUsername, new { Username = username });

    public Task<User?> GetByDisplayNameAsync(string displayName)
        => _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByDisplayName, new { DisplayName = displayName });

    public Task<int> AddAsync(User user)
        => _connection.ExecuteAsync(UserQueries.Insert, user);

    public Task<int> UpdateAsync(User user)
        => _connection.ExecuteAsync(UserQueries.Update, user);

    public async Task<int> DeleteAsync(User user)
    {
        int affectedRows = await _connection.ExecuteAsync(UserQueries.Update, user);

        // TODO: On deleted cascade for all the items of the User (Note, Label, Label_Note, Note_History)

        return affectedRows;
    }
}
