namespace Application.Access.Contracts;

public record RefreshRequest(
    string AccessToken,
    Guid RefreshToken);