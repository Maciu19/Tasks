namespace Application.Access.Contracts;

public record UserLogoutRequest(
    string AccessToken,
    Guid RefreshToken);