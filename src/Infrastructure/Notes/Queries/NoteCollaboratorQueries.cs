using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public class NoteCollaboratorQueries
{
    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.NoteCollaboratorsTableName}
        (
            note_id, 
            user_id
        )
        VALUES 
        (
            @NoteId, 
            @CollaboratorId
        )
    """;

    public const string DeleteByNoteId = $"""
        DELETE FROM {DatabaseConstants.Schema}.{DatabaseConstants.NoteCollaboratorsTableName}
        WHERE note_id = @NoteId
    """;
}
