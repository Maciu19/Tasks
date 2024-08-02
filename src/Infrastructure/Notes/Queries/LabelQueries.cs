using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public static class LabelQueries
{
    public const string Select = $"""
        SELECT
            lb.id,
            lb.user_id,
            lb.name,
            ln.note_id,
            ln.fixed
        FROM {DatabaseConstants.Schema}.{DatabaseConstants.LabelTableName} lb
            LEFT JOIN {DatabaseConstants.Schema}.{DatabaseConstants.LabelNoteTableName} ln ON lb.id = ln.label_id
    """;

    public const string SelectById = Select + " WHERE lb.id = @Id";  
    public const string SelectByUserId = Select + " WHERE lb.user_id = @UserId";
    public const string SelectByName = SelectByUserId + " AND lb.Name = @Name";

    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.LabelTableName}
        (
            user_id,
            name
        )
        VALUES
        (
            @UserId,
            @Name
        )
        RETURNING id;
    """;

    public const string Update = $"""
        UPDATE {DatabaseConstants.Schema}.{DatabaseConstants.LabelTableName}
        SET
            name = @Name
        WHERE
            id = @Id
    """;

    public const string Delete = $"""
        DELETE FROM {DatabaseConstants.Schema}.{DatabaseConstants.LabelTableName}
        WHERE
            id = @Id
    """;
}
