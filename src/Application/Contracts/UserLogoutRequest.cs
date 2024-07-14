namespace Application.Contracts;

public record UserLogoutRequest(
    string AccessToken,
    Guid RefreshToken);