namespace Infrastructure.Queries;

public static class UserQueries
{
    public const string Select = """
        SELECT id, username, email, password, display_name, deleted
        FROM public.user
        WHERE deleted = false
    """;

    public const string SelectById = Select + " AND id = @Id";
    public const string SelectByUsername = Select + " AND username = @Username";
    public const string SelectByEmail = Select + " AND email = @Email";
    public const string SelectByDisplayName = Select + " AND display_name = @DisplayName";

    public const string Insert = """
        INSERT INTO public.user
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

    public const string Update = """
        UPDATE public.user
        SET 
        	password = @Password, 
        	display_name = @DisplayName,
        	deleted = @Deleted
        WHERE id = @Id
        """;
}
