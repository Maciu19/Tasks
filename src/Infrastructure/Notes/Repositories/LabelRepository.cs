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

    private async Task<IEnumerable<Label>> GetAsync(string sql, object param)
    {
        var labels = await _connection.QueryAsync<Label, Guid?, bool?, Label>(
            sql: sql,
            (label, noteId, fix) =>
            {
                if (noteId.HasValue && fix.HasValue)
                {
                    label.AddNoteId((Guid)noteId, (bool)fix);
                }

                return label;
            },
            splitOn: "note_id, fixed",
            param: param);

        var result = labels
            .GroupBy(l => l.Id)
            .Select(g =>
            {
                var groupedLabel = g.First();

                foreach(var noteIdAndFixed in g.SelectMany(l => l.NoteIds).Distinct().ToList())
                {
                    if (groupedLabel.NoteIds.ContainsKey(noteIdAndFixed.Key))
                        continue;

                    groupedLabel.AddNoteId(noteIdAndFixed.Key, noteIdAndFixed.Value);
                }

                return groupedLabel;
            });

        return result;
    }

    public Task<IEnumerable<Label>> GetByUserIdAsync(Guid userId)
        => GetAsync(LabelQueries.SelectByUserId, new { UserId = userId });

    public async Task<Label?> GetByIdAsync(int id)
        => (await GetAsync(LabelQueries.SelectById, new { Id = id })).FirstOrDefault();

    public async Task<Label?> GetByNameAsync(Guid userId, string name)
        => (await GetAsync(LabelQueries.SelectByName, new { UserId = userId, Name = name })).FirstOrDefault();

    public Task<int> InsertAsync(Guid userId, string name)
        => _connection.ExecuteScalarAsync<int>(LabelQueries.Insert, new { UserId = userId, Name = name });

    public Task<int> UpdateAsync(int id, string newName)
        => _connection.ExecuteAsync(LabelQueries.Update, new { Id = id, Name = newName });

    public Task<int> UpdateFixedAsync(Guid noteId, int labelId, bool fix)
       => _connection.ExecuteAsync(NoteLabelQueries.UpdateFixed, new { NoteId = noteId, LabelId = labelId, Fix = fix });

    public Task<int> DeleteAsync(int id)
        => _connection.ExecuteAsync(LabelQueries.Delete, new { Id = id });
}
