namespace Application.Contracts;

public record UserLoginResponse(
    string AccessToken,
    Guid RefreshToken
);
