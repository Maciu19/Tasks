using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public static class NoteQueries
{
    public const string Select = $"""
        SELECT
    	    n.id, 
    	    n.user_id, 
    	    n.title, 
    	    n.content, 
    	    n.last_edited, 
    	    n.due_date, 
    	    n.fixed, 
    	    n.background, 
    	    n.deleted,
    	    nc.user_id AS collaborator,
    	    ln.label_id AS labelId
        FROM {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName} AS n
            LEFT JOIN {DatabaseConstants.Schema}.{DatabaseConstants.LabelNoteTableName} AS ln ON n.id = ln.note_id 
            LEFT JOIN {DatabaseConstants.Schema}.{DatabaseConstants.NoteCollaboratorsTableName} AS nc ON n.id = nc.note_id 
        WHERE n.deleted = false
    """;

    public const string SelectById = Select + " AND n.id = @Id";
    public const string SelectByUserId = Select + " AND n.user_id = @UserId";

    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName}
        (
            id, 
            user_id, 
            title, 
            content, 
            last_edited,
            due_date,
            fixed,
            background,
            deleted
        )
        VALUES
        (
            @Id,
            @UserId,
            @Title,
            @Content,
            @LastEdited,
            @DueDate,
            @Fixed,
            @Background,
            @Deleted
        )
    """;

    public const string Update = $"""   
        UPDATE {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName}
        SET
            title = @Title,
            content = @Content,
            last_edited = @LastEdited,
            fixed = @Fixed,
            background = @Background,
            due_date = @DueDate,
            deleted = @Deleted
        WHERE id = @Id
    """;
}