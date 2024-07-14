namespace Application.Contracts;

public record RefreshRequest(
    string AccessToken,
    Guid RefreshToken); 