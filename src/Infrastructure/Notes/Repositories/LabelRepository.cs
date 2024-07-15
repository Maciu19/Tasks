using Dapper;

using Domain.Notes;

using Infrastructure.Common.DatabaseProvider;
using Infrastructure.Notes.Queries;
using Infrastructure.Notes.Repositories.Abstractions;

using Npgsql;

namespace Infrastructure.Notes.Repositories;

public class LabelRepository : ILabelRepository
{
    private readonly NpgsqlConnection _connection;

    public LabelRepository(IDatabaseProvider databaseProvider)
    {
        _connection = new NpgsqlConnection(databaseProvider.GetDefaultConnectionString());
    }
    
    public Task<IEnumerable<Label>> GetByUserIdAsync(Guid userId)
        => _connection.QueryAsync<Label>(LabelQueries.SelectByUserId, new { UserId = userId });

    public Task<Label?> GetByIdAsync(int id)
        => _connection.QueryFirstOrDefaultAsync<Label>(LabelQueries.SelectById, new { Id = id });

    public Task<Label?> GetByNameAsync(Guid userId, string name)
        => _connection.QueryFirstOrDefaultAsync<Label>(LabelQueries.SelectByName, new { UserId = userId, Name = name });

    public Task<int> InsertAsync(Guid userId, string name)
        => _connection.ExecuteScalarAsync<int>(LabelQueries.Insert, new { UserId = userId, Name = name });

    public Task<int> UpdateAsync(int id, string newName)
        => _connection.ExecuteAsync(LabelQueries.Update, new { Id = id, Name = newName });

    public Task<int> DeleteAsync(int id)
        => _connection.ExecuteAsync(LabelQueries.Delete, new { Id = id });
}
