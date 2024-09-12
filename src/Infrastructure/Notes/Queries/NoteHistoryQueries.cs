using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public static class NoteHistoryQueries
{
    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.NoteHistoryTableName}
        (
            note_id, 
            title, 
            content, 
            timestamp
        )
        VALUES 
        (
            @NoteId, 
            @Title, 
            @Content, 
            @Timestamp
        )
    """;

    public const string SelectByNoteId = $"""
        SELECT
            id,
            note_id,
            title,
            content,
            timestamp
        FROM {DatabaseConstants.Schema}.{DatabaseConstants.NoteHistoryTableName}
        WHERE note_id = @NoteId
    """;
}
