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

    private Task<IEnumerable<Note>> GetAsync(string sql, object param)
        => _connection.QueryAsync<Note, Guid?, int?, Note>(
            sql: sql,
            (note, collaboratorId, labelId) =>
            {
                if (collaboratorId is not null)
                {
                    note.CollaboratorsIds.Add((Guid)collaboratorId);
                }
                if (labelId is not null)
                {
                    note.LabelsIds.Add((int) labelId);
                }

                return note;
            },
            splitOn: "collaborator, labelId",
            param: param);

    public Task<IEnumerable<Note>> GetByUserIdAsync(Guid userId)
        => GetAsync(NoteQueries.SelectByUserId, new { UserId = userId });

    public async Task<Note?> GetByIdAsync(Guid id)
        => (await GetAsync(NoteQueries.SelectById, new { Id = id })).FirstOrDefault();

    public Task<int> CreateAsync(Note note)
        => _connection.ExecuteAsync(NoteQueries.Insert, note);

    public Task<int> UpdateAsync(Note note)
        => _connection.ExecuteAsync(NoteQueries.Update, note);

    public async Task UpdateCollaboratorsAsync(Note note, IEnumerable<Guid> collaboratorsIds)
    {
        await _connection.OpenAsync();

        using var transaction = await _connection.BeginTransactionAsync();

        await _connection.ExecuteAsync(NoteCollaboratorQueries.DeleteByNoteId, new { NoteId = note.Id });

        int affectedRows = 0;

        foreach(var collaboratorId in collaboratorsIds)
        {
            affectedRows += await _connection.ExecuteAsync(NoteCollaboratorQueries.Insert, new { NoteId = note.Id, CollaboratorId = collaboratorId });
        }

        if (affectedRows != collaboratorsIds.Count())
        {
            await transaction.RollbackAsync();
            throw new Exception("Failed to update collaborators");
        }

        await transaction.CommitAsync();
    }

    public async Task UpdateLabelsAsync(Note note, IEnumerable<int> labelsIds)
    {
        await _connection.OpenAsync();

        using var transaction = await _connection.BeginTransactionAsync();

        await _connection.ExecuteAsync(NoteLabelQueries.DeleteByNoteId, new { NoteId = note.Id });

        int affectedRows = 0;

        foreach(var labelId in labelsIds)
        { 
            affectedRows += await _connection.ExecuteAsync(NoteLabelQueries.Insert, new { NoteId = note.Id, LabelId = labelId, Fixed = false });
        }

        if (affectedRows != labelsIds.Count())
        {
            await transaction.RollbackAsync();
            throw new Exception("Failed to update labels");
        }

        await transaction.CommitAsync();
    }
}
