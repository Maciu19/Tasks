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
    public async Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
        => await _connection.QueryAsync<Note>(NoteQueries.SelectByUserId, new { UserId = userId });

    public async Task<Note?> GetByIdAsync(Guid id)
        => await _connection.QueryFirstOrDefaultAsync<Note>(NoteQueries.SelectById, new { Id = id });

    public async Task<int> CreateAsync(Note note)
        => await _connection.ExecuteAsync(NoteQueries.Insert, note);

    public async Task<int> UpdateAsync(Note note)
        => await _connection.ExecuteAsync(NoteQueries.Update, note);
}
