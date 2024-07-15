using Infrastructure.Common;

namespace Infrastructure.Notes.Queries;

public static class LabelQueries
{
    public const string Select = $"""
        SELECT
            id,
            user_id,
            name
        FROM
            {DatabaseConstants.Schema}.{DatabaseConstants.LabelTableName}
    """;

    public const string SelectById = Select + " WHERE id = @Id";  
    public const string SelectByUserId = Select + " WHERE user_id = @UserId";
    public const string SelectByName = SelectByUserId + " AND Name = @Name";

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
