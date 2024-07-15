using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public static class NoteQueries
{
    public const string Select = $"""
        SELECT id, user_id, title, content, deleted, fixed, due_date
        FROM {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName}
        WHERE deleted = false
    """;

    public const string SelectById = Select + " AND id = @Id";
    public const string SelectByUserId = Select + " AND user_id = @UserId";

    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName}
        (
            id, 
            user_id, 
            title, 
            content, 
            deleted,
            fixed,
            due_date
        )
        VALUES
        (
            @Id,
            @UserId,
            @Title,
            @Content,
            @Deleted,
            @Fixed,
            @DueDate
        )
    """;

    public const string Update = $"""   
        UPDATE {DatabaseConstants.Schema}.{DatabaseConstants.NoteTableName}
        SET
            title = @Title,
            content = @Content,
            fixed = @Fixed,
            due_date = @DueDate,
            deleted = @Deleted
        WHERE id = @Id
    """;
}