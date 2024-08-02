using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public class NoteLabelQueries
{
    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.LabelNoteTableName}
        (
            note_id, 
            label_id,
            fixed
        )
        VALUES 
        (
            @NoteId, 
            @LabelId,
            @Fixed
        )
    """;

    public const string UpdateFixed = $"""
        UPDATE {DatabaseConstants.Schema}.{DatabaseConstants.LabelNoteTableName}
        SET
            fixed = @Fix
        WHERE
            note_id = @NoteId AND label_id = @LabelId
    """;

    public const string DeleteByNoteId = $"""
        DELETE FROM {DatabaseConstants.Schema}.{DatabaseConstants.LabelNoteTableName}
        WHERE note_id = @NoteId
    """;
}
