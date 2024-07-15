using Dapper;

using Domain.Notes;

using Infrastructure.Common.DatabaseProvider;
using Infrastructure.Notes.Queries;
using Infrastructure.Notes.Repositories.Abstractions;

using Npgsql;

namespace Infrastructure.Notes.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly NpgsqlConnection _connection;

    public NoteRepository(IDatabaseProvider databaseProvider)
    {
        _connection = new NpgsqlConnection(databaseProvider.GetDefaultConnectionString());
    }
    public Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
        => _connection.QueryAsync<Note>(NoteQueries.SelectByUserId, new { UserId = userId });

    public Task<Note?> GetByIdAsync(Guid id)
        => _connection.QueryFirstOrDefaultAsync<Note>(NoteQueries.SelectById, new { Id = id });

    public Task<int> CreateAsync(Note note)
        => _connection.ExecuteAsync(NoteQueries.Insert, note);

    public Task<int> UpdateAsync(Note note)
        => _connection.ExecuteAsync(NoteQueries.Update, note);
}
