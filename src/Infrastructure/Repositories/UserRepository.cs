using Dapper;

using Domain.Entities;

using Infrastructure.Common.DatabaseProvider;
using Infrastructure.Queries;

using Npgsql;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NpgsqlConnection _connection;

    public UserRepository(IDatabaseProvider databaseProvider)
    {
        _connection = new NpgsqlConnection(databaseProvider.GetDefaultConnectionString());
    }

    public async Task<User?> GetByEmailAsync(string email) 
        => await _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByEmail, new { Email = email });

    public async Task<User?> GetByIdAsync(Guid id)
        => await _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectById, new { Id = id });

    public async Task<User?> GetByUsernameAsync(string username)
        => await _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByUsername, new { Username = username } );

    public async Task<User?> GetByDisplayNameAsync(string displayName)
        => await _connection.QueryFirstOrDefaultAsync<User>(UserQueries.SelectByDisplayName, new { DisplayName = displayName });

    public async Task<int> AddAsync(User user)
        => await _connection.ExecuteAsync(UserQueries.Insert, user);

    public async Task<int> UpdateAsync(User user)
        => await _connection.ExecuteAsync(UserQueries.Update, user);

    public async Task<int> DeleteAsync(User user)
    {
        int affectedRows = await _connection.ExecuteAsync(UserQueries.Update, user);

        // TODO: On deleted cascade for all the items of the User (Note, Label, Label_Note, Note_History)

        return affectedRows;
    }
}
