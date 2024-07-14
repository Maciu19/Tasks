using Application.Errors.Common;

namespace Application.Errors;

public static class AuthorizationErrors
{
    public static Error InvalidCredentials => Error.Authentication(
       code: "User.InvalidCredentials",
       description: "Invalid email or password"
    );

    public static Error InvalidAccessToken => Error.Authentication(
        code: "User.InvalidAccessToken",
        description: "Invalid access token"
    );

    public static Error InvalidRefreshToken => Error.Authentication(
        code: "User.InvalidRefreshToken",
        description: "Invalid refresh token"
    );

    public static Error ExpiredRefreshToken => Error.Authentication(
        code: "User.ExpiredRefreshToken",
        description: "Refresh token has expired"
    );
}
