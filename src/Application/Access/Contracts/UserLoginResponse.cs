namespace Application.Access.Contracts;

public record UserLoginResponse(
    string AccessToken,
    Guid RefreshToken
);
