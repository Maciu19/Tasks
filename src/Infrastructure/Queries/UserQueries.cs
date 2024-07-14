using Infrastructure.Common;

namespace Infrastructure.Queries;

public static class UserQueries
{
    public const string Select = $"""
        SELECT id, username, email, password, display_name, deleted
        FROM {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName}
        WHERE deleted = false
    """;

    public const string SelectById = Select + " AND id = @Id";
    public const string SelectByUsername = Select + " AND username = @Username";
    public const string SelectByEmail = Select + " AND email = @Email";
    public const string SelectByDisplayName = Select + " AND display_name = @DisplayName";

    public const string Insert = $"""
        INSERT INTO {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName}
        (
            id, 
            username, 
            email, 
            password, 
            display_name, 
            deleted
        )
        VALUES
        (
            @Id,
            @Username,
            @Email,
            @Password,
            @DisplayName,
            @Deleted
        )
        """;

    public const string Update = $"""
        UPDATE {DatabaseConstants.Schema}.{DatabaseConstants.UserTableName}
        SET 
        	password = @Password, 
        	display_name = @DisplayName,
        	deleted = @Deleted
        WHERE id = @Id
        """;
}
