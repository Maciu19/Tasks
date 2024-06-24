using Application.Errors.Common;

namespace Application.Errors;

public static class UserErrors
{
    public static Error UsernameConflict(string username) => Error.Conflict(
        code: "User.UsernameConflict",
        description: $"A user with the username '{username}' already exists."
    );

    public static Error EmailConflict(string email) => Error.Conflict(
        code: "User.EmailConflict",
        description: $"A user with the email '{email}' already exists."
    );

    public static Error DisplayNameConflict(string displayName) => Error.Conflict(
        code: "User.DisplayNameConflict",
        description: $"A user with the display name '{displayName}' already exists."
    );

    public static Error NotFound(string message) => Error.NotFound(
        code: "User.NotFound",
        description: message
    );

    public static Error InvalidCredentials => Error.Authentication(
        code: "User.InvalidCredentials",
        description: "Invalid email or password"
    );
}
